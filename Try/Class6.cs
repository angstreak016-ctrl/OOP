using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Try
{
    public class WorkoutComboWatcher : IDisposable
    {
        public const string BodyPartTag = "BodyPart";
        public const string WorkoutTag = "Workout";

        private readonly Control _root;
        private readonly Func<string> _usernameProvider;
        private readonly HashSet<Control> _watched = new HashSet<Control>();
        private readonly HashSet<ComboBox> _hooked = new HashSet<ComboBox>();

        /// <summary>Finds the workout combo box that belongs to a body part combo box.</summary>
        public Func<ComboBox, ComboBox> TargetResolver { get; set; }

        public WorkoutComboWatcher(Control root, Func<string> usernameProvider)
        {
            _root = root ?? throw new ArgumentNullException(nameof(root));
            _usernameProvider = usernameProvider ?? throw new ArgumentNullException(nameof(usernameProvider));
            TargetResolver = DefaultTargetResolver;
        }

        public void Start() => Watch(_root);

        public void Dispose() => Unwatch(_root);

        // ---------- Scanning ----------

        private void Watch(Control control)
        {
            if (!_watched.Add(control)) return;

            control.ControlAdded += OnControlAdded;
            control.ControlRemoved += OnControlRemoved;

            if (control is ComboBox combo && _hooked.Add(combo))
                combo.SelectedIndexChanged += Combo_SelectedIndexChanged;

            foreach (Control child in control.Controls)
                Watch(child);
        }

        private void Unwatch(Control control)
        {
            if (!_watched.Remove(control)) return;

            control.ControlAdded -= OnControlAdded;
            control.ControlRemoved -= OnControlRemoved;

            if (control is ComboBox combo && _hooked.Remove(combo))
                combo.SelectedIndexChanged -= Combo_SelectedIndexChanged;

            foreach (Control child in control.Controls)
                Unwatch(child);
        }

        private void OnControlAdded(object sender, ControlEventArgs e) => Watch(e.Control);
        private void OnControlRemoved(object sender, ControlEventArgs e) => Unwatch(e.Control);

        // ---------- Event handling ----------

        private void Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var source = (ComboBox)sender;

            // Tag is checked at event time, so it works even if it's set after the control is added
            if (!HasTag(source, BodyPartTag) || source.SelectedItem == null) return;

            ComboBox target = TargetResolver(source);
            if (target == null) return;

            // GetItemText works whether the items are strings or bound DataRowViews
            LoadWorkouts(source.GetItemText(source.SelectedItem), target);
        }

        private void LoadWorkouts(string bodyPart, ComboBox target)
        {
            string username = _usernameProvider()?.Trim();
            if (string.IsNullOrEmpty(username)) return;

            Database db = new Database();
            db.openConnection();

            if (db.con == null || db.con.State != ConnectionState.Open) return;

            try
            {
                int maxPoints = GetMaxPoints(GetUserRank(db, username));

                const string query =
                    "SELECT name FROM Workouts WHERE bodypart = @Bodypart AND points <= @Points";

                using (SqlCommand cmd = new SqlCommand(query, db.con))
                {
                    cmd.Parameters.AddWithValue("@Bodypart", bodyPart);
                    cmd.Parameters.AddWithValue("@Points", maxPoints);

                    DataTable dt = new DataTable();
                    new SqlDataAdapter(cmd).Fill(dt);

                    target.DataSource = dt;
                    target.DisplayMember = "name";
                    target.ValueMember = "name";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading workouts: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.con.Close();
            }
        }

        // ---------- Helpers ----------

        private static int GetUserRank(Database db, string username)
        {
            const string query = "SELECT UserRank FROM Users WHERE Username = @Username";

            using (SqlCommand cmd = new SqlCommand(query, db.con))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        private static int GetMaxPoints(int rank)
        {
            if (rank > 400) return 20;   // Advanced
            if (rank >= 300) return 12;  // Intermediate
            return 5;                    // Beginner
        }

        private static bool HasTag(Control c, string tag) =>
            string.Equals(c.Tag as string, tag, StringComparison.OrdinalIgnoreCase);

        private ComboBox DefaultTargetResolver(ComboBox source)
        {
            if (source.Parent == null) return null;

            foreach (Control c in source.Parent.Controls)
                if (c is ComboBox cb && cb != source && HasTag(cb, WorkoutTag))
                    return cb;

            return null;
        }
    }
}
