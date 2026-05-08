using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ULMSWinFormsAppClient_Zandro
{
    public partial class MainForm : Form
    {
        
        // DATA
        
        private readonly Dictionary<string, string> _users = new()
        {
            { "admin", "Admin123" },
            { "student", "Student123" }
        };

        private readonly List<Student> _students = new();
        private readonly List<Enrollment> _enrollments = new();
        private readonly List<MarkRecord> _marks = new();

        private readonly string[] _courses =
        {
            "Software Development",
            "Database Systems",
            "Software Testing",
            "Networking Fundamentals"
        };

        private string _currentUser = "";

        // UI
       
        private Panel _loginPanel = null!;
        private Panel _loginCard = null!;
        private TextBox _txtUsername = null!;
        private TextBox _txtPassword = null!;
        private Label _lblLoginStatus = null!;

        private Panel _appPanel = null!;
        private Panel _headerPanel = null!;
        private Label _lblHeaderTitle = null!;
        private Label _lblCurrentUser = null!;
        private Button _btnLogout = null!;
        private TabControl _tabControl = null!;

        private StatusStrip _statusStrip = null!;
        private ToolStripStatusLabel _statusLabel = null!;

        
        //  THE DASHBOARD
   
        private Label _lblDashboardSummary = null!;

        
        // STUDENTS TAB
       
        private TextBox _txtStudentNumber = null!;
        private TextBox _txtStudentName = null!;
        private TextBox _txtStudentEmail = null!;
        private Button _btnRegisterStudent = null!;
        private Button _btnClearStudent = null!;
        private Label _lblStudentTabStatus = null!;
        private ListBox _lstStudents = null!;

       
        // ENROLMENT TAB
       
        private ComboBox _cmbEnrollStudent = null!;
        private ComboBox _cmbEnrollCourse = null!;
        private Button _btnEnroll = null!;
        private Label _lblEnrollStatus = null!;
        private ListBox _lstEnrollments = null!;

        // MARKS TAB
       
        private ComboBox _cmbMarksStudent = null!;
        private ComboBox _cmbMarksCourse = null!;
        private TextBox _txtMark = null!;
        private Button _btnSaveMark = null!;
        private Button _btnCalculateAverage = null!;
        private Label _lblMarksStatus = null!;
        private ListBox _lstMarks = null!;

     
        // REPORTS TAB
       
        private Button _btnGenerateReport = null!;
        private TextBox _txtReport = null!;
        private Label _lblReportStatus = null!;

        public MainForm()
        {
            Text = "Umoja Learning Management System (ULMS)";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1280, 780);
            MinimumSize = new Size(1180, 720);
            BackColor = Color.FromArgb(240, 242, 245);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            FormBorderStyle = FormBorderStyle.Sizable;

            BuildLoginPanel();
            BuildApplicationPanel();
            BuildStatusBar();

            Controls.Add(_loginPanel);
            Controls.Add(_appPanel);
            Controls.Add(_statusStrip);

            Resize += MainForm_Resize;

            ShowLogin();
        }

     
        // LOGIN PANEL
    
        private void BuildLoginPanel()
        {
            _loginPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(240, 242, 245)
            };

            _loginCard = new Panel
            {
                Size = new Size(460, 320),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblTitle = new Label
            {
                Text = "Umoja Learning Management System",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(28, 24)
            };

            Label lblSubtitle = new Label
            {
                Text = "Login to continue",
                ForeColor = Color.DimGray,
                AutoSize = true,
                Location = new Point(30, 60)
            };

            Label lblUsername = new Label
            {
                Text = "Username",
                AutoSize = true,
                Location = new Point(30, 100)
            };

            _txtUsername = new TextBox
            {
                Location = new Point(30, 122),
                Width = 390
            };

            Label lblPassword = new Label
            {
                Text = "Password",
                AutoSize = true,
                Location = new Point(30, 160)
            };

            _txtPassword = new TextBox
            {
                Location = new Point(30, 182),
                Width = 390,
                UseSystemPasswordChar = true
            };

            Button btnLogin = new Button
            {
                Text = "Login",
                Location = new Point(30, 225),
                Size = new Size(390, 38),
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += BtnLogin_Click;

            _lblLoginStatus = new Label
            {
                AutoSize = true,
                ForeColor = Color.DarkRed,
                Location = new Point(30, 275)
            };

            _loginCard.Controls.Add(lblTitle);
            _loginCard.Controls.Add(lblSubtitle);
            _loginCard.Controls.Add(lblUsername);
            _loginCard.Controls.Add(_txtUsername);
            _loginCard.Controls.Add(lblPassword);
            _loginCard.Controls.Add(_txtPassword);
            _loginCard.Controls.Add(btnLogin);
            _loginCard.Controls.Add(_lblLoginStatus);

            _loginPanel.Controls.Add(_loginCard);

            CenterLoginCard();
        }

     
        // APP PANEL
       
        private void BuildApplicationPanel()
        {
            _appPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Visible = false,
                BackColor = Color.FromArgb(240, 242, 245)
            };

            _headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 58,
                BackColor = Color.FromArgb(28, 28, 28)
            };
            _headerPanel.Resize += (s, e) => UpdateHeaderLayout();

            _lblHeaderTitle = new Label
            {
                Text = "Umoja Learning Management System",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(18, 12)
            };

            _lblCurrentUser = new Label
            {
                Text = "Logged in as: -",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                AutoSize = true,
                Top = 19
            };

            _btnLogout = new Button
            {
                Text = "Logout",
                Size = new Size(110, 34),
                Top = 12,
                BackColor = Color.FromArgb(192, 57, 43),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            _btnLogout.FlatAppearance.BorderSize = 0;
            _btnLogout.FlatAppearance.MouseOverBackColor = Color.FromArgb(165, 48, 37);
            _btnLogout.FlatAppearance.MouseDownBackColor = Color.FromArgb(140, 40, 30);
            _btnLogout.Click += BtnLogout_Click;

            _headerPanel.Controls.Add(_lblHeaderTitle);
            _headerPanel.Controls.Add(_lblCurrentUser);
            _headerPanel.Controls.Add(_btnLogout);

            Panel body = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(18, 16, 18, 12),
                BackColor = Color.FromArgb(240, 242, 245)
            };

            _tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular)
            };

            _tabControl.TabPages.Add(BuildDashboardTab());
            _tabControl.TabPages.Add(BuildStudentsTab());
            _tabControl.TabPages.Add(BuildEnrollmentTab());
            _tabControl.TabPages.Add(BuildMarksTab());
            _tabControl.TabPages.Add(BuildReportsTab());

            body.Controls.Add(_tabControl);

            _appPanel.Controls.Add(body);
            _appPanel.Controls.Add(_headerPanel);
        }

        private void BuildStatusBar()
        {
            _statusStrip = new StatusStrip();

            _statusLabel = new ToolStripStatusLabel
            {
                Text = "Ready"
            };

            _statusStrip.Items.Add(_statusLabel);
        }

      
        // TAB BUILDERS
    
        private TabPage BuildDashboardTab()
        {
            TabPage tab = new TabPage("Dashboard");

            Panel card = CreateCardPanel();
            card.Dock = DockStyle.Fill;

            Label lblHeading = new Label
            {
                Text = "Dashboard Summary",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(24, 22)
            };

            Label lblInfo = new Label
            {
                Text = "Use this app to test login validation, student registration, course enrolment, marks capture, average calculation, and report generation.",
                AutoSize = false,
                Size = new Size(1080, 45),
                Location = new Point(24, 58)
            };

            Button btnLoadDemo = new Button
            {
                Text = "Load Demo",
                Location = new Point(24, 116),
                Size = new Size(110, 36)
            };
            btnLoadDemo.Click += BtnLoadDemo_Click;

            _lblDashboardSummary = new Label
            {
                Text = "No data loaded yet.",
                AutoSize = false,
                Size = new Size(1080, 340),
                Location = new Point(24, 180),
                Font = new Font("Segoe UI", 11F, FontStyle.Regular)
            };

            card.Controls.Add(lblHeading);
            card.Controls.Add(lblInfo);
            card.Controls.Add(btnLoadDemo);
            card.Controls.Add(_lblDashboardSummary);

            tab.Controls.Add(card);
            return tab;
        }

        private TabPage BuildStudentsTab()
        {
            TabPage tab = new TabPage("Students");

            Panel outer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(240, 242, 245)
            };

            Panel leftCard = CreateCardPanel();
            leftCard.Location = new Point(18, 18);
            leftCard.Size = new Size(380, 520);

            Label lblHeading = new Label
            {
                Text = "Register Student",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(22, 20)
            };

            Label lblStudentNumber = new Label
            {
                Text = "Student Number",
                AutoSize = true,
                Location = new Point(22, 75)
            };

            _txtStudentNumber = new TextBox
            {
                Location = new Point(22, 98),
                Width = 320
            };

            Label lblFullName = new Label
            {
                Text = "Full Name",
                AutoSize = true,
                Location = new Point(22, 142)
            };

            _txtStudentName = new TextBox
            {
                Location = new Point(22, 165),
                Width = 320
            };

            Label lblEmail = new Label
            {
                Text = "Email",
                AutoSize = true,
                Location = new Point(22, 209)
            };

            _txtStudentEmail = new TextBox
            {
                Location = new Point(22, 232),
                Width = 320
            };

            _btnRegisterStudent = new Button
            {
                Text = "Register Student",
                Location = new Point(22, 285),
                Size = new Size(155, 36)
            };
            _btnRegisterStudent.Click += BtnRegisterStudent_Click;

            _btnClearStudent = new Button
            {
                Text = "Clear",
                Location = new Point(187, 285),
                Size = new Size(155, 36)
            };
            _btnClearStudent.Click += BtnClearStudent_Click;

            _lblStudentTabStatus = new Label
            {
                AutoSize = false,
                Size = new Size(320, 70),
                Location = new Point(22, 340),
                ForeColor = Color.DarkRed
            };

            leftCard.Controls.Add(lblHeading);
            leftCard.Controls.Add(lblStudentNumber);
            leftCard.Controls.Add(_txtStudentNumber);
            leftCard.Controls.Add(lblFullName);
            leftCard.Controls.Add(_txtStudentName);
            leftCard.Controls.Add(lblEmail);
            leftCard.Controls.Add(_txtStudentEmail);
            leftCard.Controls.Add(_btnRegisterStudent);
            leftCard.Controls.Add(_btnClearStudent);
            leftCard.Controls.Add(_lblStudentTabStatus);

            Panel rightCard = CreateCardPanel();
            rightCard.Location = new Point(416, 18);
            rightCard.Size = new Size(790, 520);

            Label lblListHeading = new Label
            {
                Text = "Registered Students",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(22, 20)
            };

            _lstStudents = new ListBox
            {
                Location = new Point(22, 60),
                Size = new Size(740, 430),
                Font = new Font("Segoe UI", 10F)
            };

            rightCard.Controls.Add(lblListHeading);
            rightCard.Controls.Add(_lstStudents);

            outer.Controls.Add(leftCard);
            outer.Controls.Add(rightCard);

            tab.Controls.Add(outer);
            return tab;
        }

        private TabPage BuildEnrollmentTab()
        {
            TabPage tab = new TabPage("Course Enrolment");

            Panel outer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(240, 242, 245)
            };

            Panel leftCard = CreateCardPanel();
            leftCard.Location = new Point(18, 18);
            leftCard.Size = new Size(380, 520);

            Label lblHeading = new Label
            {
                Text = "Course Enrolment",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(22, 20)
            };

            Label lblStudent = new Label
            {
                Text = "Student",
                AutoSize = true,
                Location = new Point(22, 85)
            };

            _cmbEnrollStudent = new ComboBox
            {
                Location = new Point(22, 108),
                Width = 320,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            Label lblCourse = new Label
            {
                Text = "Course",
                AutoSize = true,
                Location = new Point(22, 155)
            };

            _cmbEnrollCourse = new ComboBox
            {
                Location = new Point(22, 178),
                Width = 320,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            _cmbEnrollCourse.Items.AddRange(_courses);

            _btnEnroll = new Button
            {
                Text = "Enrol Student",
                Location = new Point(22, 235),
                Size = new Size(320, 36)
            };
            _btnEnroll.Click += BtnEnroll_Click;

            _lblEnrollStatus = new Label
            {
                AutoSize = false,
                Size = new Size(320, 70),
                Location = new Point(22, 292),
                ForeColor = Color.DarkRed
            };

            leftCard.Controls.Add(lblHeading);
            leftCard.Controls.Add(lblStudent);
            leftCard.Controls.Add(_cmbEnrollStudent);
            leftCard.Controls.Add(lblCourse);
            leftCard.Controls.Add(_cmbEnrollCourse);
            leftCard.Controls.Add(_btnEnroll);
            leftCard.Controls.Add(_lblEnrollStatus);

            Panel rightCard = CreateCardPanel();
            rightCard.Location = new Point(416, 18);
            rightCard.Size = new Size(790, 520);

            Label lblListHeading = new Label
            {
                Text = "Captured Enrolments",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(22, 20)
            };

            _lstEnrollments = new ListBox
            {
                Location = new Point(22, 60),
                Size = new Size(740, 430),
                Font = new Font("Segoe UI", 10F)
            };

            rightCard.Controls.Add(lblListHeading);
            rightCard.Controls.Add(_lstEnrollments);

            outer.Controls.Add(leftCard);
            outer.Controls.Add(rightCard);

            tab.Controls.Add(outer);
            return tab;
        }

        private TabPage BuildMarksTab()
        {
            TabPage tab = new TabPage("Marks Capture");

            Panel outer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(240, 242, 245)
            };

            Panel leftCard = CreateCardPanel();
            leftCard.Location = new Point(18, 18);
            leftCard.Size = new Size(380, 520);

            Label lblHeading = new Label
            {
                Text = "Capture Marks",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(22, 20)
            };

            Label lblStudent = new Label
            {
                Text = "Student",
                AutoSize = true,
                Location = new Point(22, 78)
            };

            _cmbMarksStudent = new ComboBox
            {
                Location = new Point(22, 101),
                Width = 320,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            Label lblCourse = new Label
            {
                Text = "Course",
                AutoSize = true,
                Location = new Point(22, 148)
            };

            _cmbMarksCourse = new ComboBox
            {
                Location = new Point(22, 171),
                Width = 320,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            _cmbMarksCourse.Items.AddRange(_courses);

            Label lblMark = new Label
            {
                Text = "Mark (%)",
                AutoSize = true,
                Location = new Point(22, 218)
            };

            _txtMark = new TextBox
            {
                Location = new Point(22, 241),
                Width = 320
            };

            _btnSaveMark = new Button
            {
                Text = "Save Mark",
                Location = new Point(22, 294),
                Size = new Size(155, 36)
            };
            _btnSaveMark.Click += BtnSaveMark_Click;

            _btnCalculateAverage = new Button
            {
                Text = "Calculate Average",
                Location = new Point(187, 294),
                Size = new Size(155, 36)
            };
            _btnCalculateAverage.Click += BtnCalculateAverage_Click;

            _lblMarksStatus = new Label
            {
                AutoSize = false,
                Size = new Size(320, 90),
                Location = new Point(22, 350),
                ForeColor = Color.DarkRed
            };

            leftCard.Controls.Add(lblHeading);
            leftCard.Controls.Add(lblStudent);
            leftCard.Controls.Add(_cmbMarksStudent);
            leftCard.Controls.Add(lblCourse);
            leftCard.Controls.Add(_cmbMarksCourse);
            leftCard.Controls.Add(lblMark);
            leftCard.Controls.Add(_txtMark);
            leftCard.Controls.Add(_btnSaveMark);
            leftCard.Controls.Add(_btnCalculateAverage);
            leftCard.Controls.Add(_lblMarksStatus);

            Panel rightCard = CreateCardPanel();
            rightCard.Location = new Point(416, 18);
            rightCard.Size = new Size(790, 520);

            Label lblListHeading = new Label
            {
                Text = "Captured Marks",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(22, 20)
            };

            _lstMarks = new ListBox
            {
                Location = new Point(22, 60),
                Size = new Size(740, 430),
                Font = new Font("Segoe UI", 10F)
            };

            rightCard.Controls.Add(lblListHeading);
            rightCard.Controls.Add(_lstMarks);

            outer.Controls.Add(leftCard);
            outer.Controls.Add(rightCard);

            tab.Controls.Add(outer);
            return tab;
        }

        private TabPage BuildReportsTab()
        {
            TabPage tab = new TabPage("Reports");

            Panel card = CreateCardPanel();
            card.Dock = DockStyle.Fill;

            Label lblHeading = new Label
            {
                Text = "Reports",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(24, 20)
            };

            _btnGenerateReport = new Button
            {
                Text = "Generate Report",
                Location = new Point(24, 62),
                Size = new Size(150, 36)
            };
            _btnGenerateReport.Click += BtnGenerateReport_Click;

            _lblReportStatus = new Label
            {
                AutoSize = true,
                Location = new Point(190, 70),
                ForeColor = Color.DarkGreen
            };

            _txtReport = new TextBox
            {
                Location = new Point(24, 115),
                Size = new Size(1120, 410),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Consolas", 10F),
                ReadOnly = true
            };

            card.Controls.Add(lblHeading);
            card.Controls.Add(_btnGenerateReport);
            card.Controls.Add(_lblReportStatus);
            card.Controls.Add(_txtReport);

            tab.Controls.Add(card);
            return tab;
        }

      
        // HELPER 
        
        private Panel CreateCardPanel()
        {
            return new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
        }

        private void CenterLoginCard()
        {
            if (_loginCard == null || _loginPanel == null) return;

            _loginCard.Left = (_loginPanel.ClientSize.Width - _loginCard.Width) / 2;
            _loginCard.Top = (_loginPanel.ClientSize.Height - _loginCard.Height) / 2 - 20;

            if (_loginCard.Top < 20)
                _loginCard.Top = 20;
        }

        private void UpdateHeaderLayout()
        {
            if (_headerPanel == null || _btnLogout == null || _lblCurrentUser == null || _lblHeaderTitle == null)
                return;

            _btnLogout.Left = _headerPanel.Width - _btnLogout.Width - 18;
            _btnLogout.Top = 12;

            _lblCurrentUser.Top = 19;

            int minLeft = _lblHeaderTitle.Right + 30;
            int preferredLeft = _btnLogout.Left - _lblCurrentUser.PreferredWidth - 18;

            _lblCurrentUser.Left = Math.Max(minLeft, preferredLeft);
        }

        private void SetStatus(string message)
        {
            _statusLabel.Text = $"{DateTime.Now:HH:mm:ss} - {message}";
        }

        private void RefreshStudentViews()
        {
            _lstStudents.Items.Clear();

            foreach (var student in _students.OrderBy(s => s.StudentNumber))
            {
                _lstStudents.Items.Add($"{student.StudentNumber} - {student.FullName} ({student.Email})");
            }

            var studentDisplayList = _students
                .OrderBy(s => s.StudentNumber)
                .Select(s => new ComboItem(s.StudentNumber, $"{s.StudentNumber} - {s.FullName}"))
                .ToList();

            _cmbEnrollStudent.DataSource = null;
            _cmbEnrollStudent.DataSource = studentDisplayList.ToList();
            _cmbEnrollStudent.DisplayMember = "Display";
            _cmbEnrollStudent.ValueMember = "Value";

            _cmbMarksStudent.DataSource = null;
            _cmbMarksStudent.DataSource = studentDisplayList.ToList();
            _cmbMarksStudent.DisplayMember = "Display";
            _cmbMarksStudent.ValueMember = "Value";

            UpdateDashboardSummary();
        }

        private void RefreshEnrollmentViews()
        {
            _lstEnrollments.Items.Clear();

            foreach (var enrollment in _enrollments.OrderBy(e => e.StudentNumber).ThenBy(e => e.CourseName))
            {
                var student = _students.FirstOrDefault(s => s.StudentNumber == enrollment.StudentNumber);
                string studentText = student == null
                    ? enrollment.StudentNumber
                    : $"{student.StudentNumber} - {student.FullName}";

                _lstEnrollments.Items.Add($"{studentText} | {enrollment.CourseName}");
            }

            UpdateDashboardSummary();
        }

        private void RefreshMarkViews()
        {
            _lstMarks.Items.Clear();

            foreach (var mark in _marks.OrderBy(m => m.StudentNumber).ThenBy(m => m.CourseName))
            {
                var student = _students.FirstOrDefault(s => s.StudentNumber == mark.StudentNumber);
                string studentText = student == null
                    ? mark.StudentNumber
                    : $"{student.StudentNumber} - {student.FullName}";

                _lstMarks.Items.Add($"{studentText} | {mark.CourseName} | {mark.MarkValue:F2}%");
            }

            UpdateDashboardSummary();
        }

        private void RefreshAllViews()
        {
            RefreshStudentViews();
            RefreshEnrollmentViews();
            RefreshMarkViews();
        }

        private void UpdateDashboardSummary()
        {
            int studentCount = _students.Count;
            int enrollmentCount = _enrollments.Count;
            int markCount = _marks.Count;

            double overallAverage = _marks.Count > 0 ? _marks.Average(m => m.MarkValue) : 0;

            _lblDashboardSummary.Text =
                $"Students Registered: {studentCount}\r\n\r\n" +
                $"Course Enrolments: {enrollmentCount}\r\n\r\n" +
                $"Marks Captured: {markCount}\r\n\r\n" +
                $"Overall Average: {(markCount > 0 ? overallAverage.ToString("F2") + "%" : "N/A")}\r\n\r\n" +
                "Tip: Use 'Load Demo' if you want quick sample records for screenshots and testing.";
        }

        private void ClearStudentFields(bool clearStatus = true)
        {
            _txtStudentNumber.Clear();
            _txtStudentName.Clear();
            _txtStudentEmail.Clear();

            if (clearStatus)
                _lblStudentTabStatus.Text = "";
        }

        private void ShowLogin()
        {
            _appPanel.Visible = false;
            _loginPanel.Visible = true;

            _txtUsername.Clear();
            _txtPassword.Clear();
            _lblLoginStatus.Text = "";
            _txtUsername.Focus();

            CenterLoginCard();
            SetStatus("Ready");
        }

        private void ShowApplication()
        {
            _lblCurrentUser.Text = $"Logged in as: {_currentUser}";
            _loginPanel.Visible = false;
            _appPanel.Visible = true;
            _tabControl.SelectedIndex = 0;
            UpdateDashboardSummary();
            UpdateHeaderLayout();
            SetStatus("Login successful");
        }

      
        // EVENTS
       
        private void MainForm_Resize(object? sender, EventArgs e)
        {
            CenterLoginCard();
            UpdateHeaderLayout();
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            string username = _txtUsername.Text.Trim();
            string password = _txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) && string.IsNullOrWhiteSpace(password))
            {
                _lblLoginStatus.ForeColor = Color.DarkRed;
                _lblLoginStatus.Text = "Username and password are required.";
                SetStatus("Login validation failed");
                return;
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                _lblLoginStatus.ForeColor = Color.DarkRed;
                _lblLoginStatus.Text = "Username is required.";
                SetStatus("Login validation failed");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                _lblLoginStatus.ForeColor = Color.DarkRed;
                _lblLoginStatus.Text = "Password is required.";
                SetStatus("Login validation failed");
                return;
            }

            if (_users.TryGetValue(username, out var savedPassword) && savedPassword == password)
            {
                _currentUser = username;
                ShowApplication();
            }
            else
            {
                _lblLoginStatus.ForeColor = Color.DarkRed;
                _lblLoginStatus.Text = "Invalid username or password.";
                SetStatus("Invalid login attempt");
            }
        }

        private void BtnLogout_Click(object? sender, EventArgs e)
        {
            _currentUser = "";
            ShowLogin();
        }

        private void BtnLoadDemo_Click(object? sender, EventArgs e)
        {
            _students.Clear();
            _enrollments.Clear();
            _marks.Clear();

            _students.AddRange(new[]
            {
                new Student { StudentNumber = "ST1001", FullName = "Thabo Mokoena", Email = "thabo@student.co.za" },
                new Student { StudentNumber = "ST1002", FullName = "Aisha Naidoo", Email = "aisha@student.co.za" },
                new Student { StudentNumber = "ST1003", FullName = "Zandro Spinner", Email = "zandro@student.co.za" }
            });

            _enrollments.AddRange(new[]
            {
                new Enrollment { StudentNumber = "ST1001", CourseName = "Software Development" },
                new Enrollment { StudentNumber = "ST1002", CourseName = "Database Systems" },
                new Enrollment { StudentNumber = "ST1003", CourseName = "Software Testing" }
            });

            _marks.AddRange(new[]
            {
                new MarkRecord { StudentNumber = "ST1001", CourseName = "Software Development", MarkValue = 74 },
                new MarkRecord { StudentNumber = "ST1002", CourseName = "Database Systems", MarkValue = 81 },
                new MarkRecord { StudentNumber = "ST1003", CourseName = "Software Testing", MarkValue = 68 }
            });

            RefreshAllViews();
            SetStatus("Demo data loaded");
            MessageBox.Show("Demo data loaded successfully.", "ULMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnRegisterStudent_Click(object? sender, EventArgs e)
        {
            string studentNumber = _txtStudentNumber.Text.Trim();
            string fullName = _txtStudentName.Text.Trim();
            string email = _txtStudentEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(studentNumber) ||
                string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(email))
            {
                _lblStudentTabStatus.ForeColor = Color.DarkRed;
                _lblStudentTabStatus.Text = "All student fields are required.";
                SetStatus("Student registration failed");
                return;
            }

            if (_students.Any(s => s.StudentNumber.Equals(studentNumber, StringComparison.OrdinalIgnoreCase)))
            {
                _lblStudentTabStatus.ForeColor = Color.DarkRed;
                _lblStudentTabStatus.Text = "Duplicate student number blocked.";
                SetStatus("Duplicate student blocked");
                return;
            }

            _students.Add(new Student
            {
                StudentNumber = studentNumber,
                FullName = fullName,
                Email = email
            });

            RefreshStudentViews();
            _lblStudentTabStatus.ForeColor = Color.DarkGreen;
            _lblStudentTabStatus.Text = "Student registered successfully.";
            SetStatus("Student registered");
            ClearStudentFields(false);
        }

        private void BtnClearStudent_Click(object? sender, EventArgs e)
        {
            ClearStudentFields();
            SetStatus("Student form cleared");
        }

        private void BtnEnroll_Click(object? sender, EventArgs e)
        {
            if (_cmbEnrollStudent.SelectedItem == null || _cmbEnrollCourse.SelectedItem == null)
            {
                _lblEnrollStatus.ForeColor = Color.DarkRed;
                _lblEnrollStatus.Text = "Please select a student and a course.";
                SetStatus("Course enrolment failed");
                return;
            }

            string studentNumber = ((ComboItem)_cmbEnrollStudent.SelectedItem).Value;
            string courseName = _cmbEnrollCourse.SelectedItem.ToString() ?? "";

            if (_enrollments.Any(e1 => e1.StudentNumber == studentNumber && e1.CourseName == courseName))
            {
                _lblEnrollStatus.ForeColor = Color.DarkRed;
                _lblEnrollStatus.Text = "Duplicate course enrolment blocked.";
                SetStatus("Duplicate enrolment blocked");
                return;
            }

            _enrollments.Add(new Enrollment
            {
                StudentNumber = studentNumber,
                CourseName = courseName
            });

            RefreshEnrollmentViews();
            _lblEnrollStatus.ForeColor = Color.DarkGreen;
            _lblEnrollStatus.Text = "Student enrolled successfully.";
            SetStatus("Course enrolment saved");
        }

        private void BtnSaveMark_Click(object? sender, EventArgs e)
        {
            if (_cmbMarksStudent.SelectedItem == null || _cmbMarksCourse.SelectedItem == null)
            {
                _lblMarksStatus.ForeColor = Color.DarkRed;
                _lblMarksStatus.Text = "Please select a student and a course.";
                SetStatus("Marks capture failed");
                return;
            }

            string studentNumber = ((ComboItem)_cmbMarksStudent.SelectedItem).Value;
            string courseName = _cmbMarksCourse.SelectedItem.ToString() ?? "";

            if (!double.TryParse(_txtMark.Text.Trim(), out double markValue))
            {
                _lblMarksStatus.ForeColor = Color.DarkRed;
                _lblMarksStatus.Text = "Mark must be numeric.";
                SetStatus("Marks capture failed");
                return;
            }

            if (markValue < 0 || markValue > 100)
            {
                _lblMarksStatus.ForeColor = Color.DarkRed;
                _lblMarksStatus.Text = "Mark must be between 0 and 100.";
                SetStatus("Marks capture failed");
                return;
            }

            var existing = _marks.FirstOrDefault(m => m.StudentNumber == studentNumber && m.CourseName == courseName);

            if (existing == null)
            {
                _marks.Add(new MarkRecord
                {
                    StudentNumber = studentNumber,
                    CourseName = courseName,
                    MarkValue = markValue
                });

                _lblMarksStatus.ForeColor = Color.DarkGreen;
                _lblMarksStatus.Text = "Mark captured successfully.";
            }
            else
            {
                existing.MarkValue = markValue;
                _lblMarksStatus.ForeColor = Color.DarkGreen;
                _lblMarksStatus.Text = "Existing mark updated successfully.";
            }

            RefreshMarkViews();
            _txtMark.Clear();
            SetStatus("Marks saved");
        }

        private void BtnCalculateAverage_Click(object? sender, EventArgs e)
        {
            if (_cmbMarksStudent.SelectedItem == null)
            {
                _lblMarksStatus.ForeColor = Color.DarkRed;
                _lblMarksStatus.Text = "Please select a student.";
                SetStatus("Average calculation failed");
                return;
            }

            string studentNumber = ((ComboItem)_cmbMarksStudent.SelectedItem).Value;

            var studentMarks = _marks.Where(m => m.StudentNumber == studentNumber).ToList();

            if (studentMarks.Count == 0)
            {
                _lblMarksStatus.ForeColor = Color.DarkRed;
                _lblMarksStatus.Text = "No marks found for selected student.";
                SetStatus("Average calculation failed");
                return;
            }

            double average = studentMarks.Average(m => m.MarkValue);
            string result = average >= 50 ? "PASS" : "FAIL";

            _lblMarksStatus.ForeColor = Color.DarkGreen;
            _lblMarksStatus.Text = $"Average = {average:F2}% ({result})";
            SetStatus("Average calculated");
        }

        private void BtnGenerateReport_Click(object? sender, EventArgs e)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UMOJA LEARNING MANAGEMENT SYSTEM REPORT");
            sb.AppendLine($"Generated By: {_currentUser}");
            sb.AppendLine($"Generated On: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine(new string('-', 70));
            sb.AppendLine();

            sb.AppendLine("REGISTERED STUDENTS");
            sb.AppendLine(new string('-', 70));

            if (_students.Count == 0)
            {
                sb.AppendLine("No students registered.");
            }
            else
            {
                foreach (var student in _students.OrderBy(s => s.StudentNumber))
                {
                    sb.AppendLine($"{student.StudentNumber} | {student.FullName} | {student.Email}");
                }
            }

            sb.AppendLine();
            sb.AppendLine("COURSE ENROLMENTS");
            sb.AppendLine(new string('-', 70));

            if (_enrollments.Count == 0)
            {
                sb.AppendLine("No course enrolments captured.");
            }
            else
            {
                foreach (var enrollment in _enrollments.OrderBy(e1 => e1.StudentNumber).ThenBy(e1 => e1.CourseName))
                {
                    sb.AppendLine($"{enrollment.StudentNumber} | {enrollment.CourseName}");
                }
            }

            sb.AppendLine();
            sb.AppendLine("MARKS");
            sb.AppendLine(new string('-', 70));

            if (_marks.Count == 0)
            {
                sb.AppendLine("No marks captured.");
            }
            else
            {
                foreach (var mark in _marks.OrderBy(m => m.StudentNumber).ThenBy(m => m.CourseName))
                {
                    sb.AppendLine($"{mark.StudentNumber} | {mark.CourseName} | {mark.MarkValue:F2}%");
                }
            }

            sb.AppendLine();
            sb.AppendLine("AVERAGES");
            sb.AppendLine(new string('-', 70));

            var groupedMarks = _marks
                .GroupBy(m => m.StudentNumber)
                .OrderBy(g => g.Key)
                .ToList();

            if (groupedMarks.Count == 0)
            {
                sb.AppendLine("No averages available.");
            }
            else
            {
                foreach (var group in groupedMarks)
                {
                    double avg = group.Average(x => x.MarkValue);
                    string result = avg >= 50 ? "PASS" : "FAIL";
                    sb.AppendLine($"{group.Key} | Average: {avg:F2}% | {result}");
                }
            }

            stopwatch.Stop();

            _txtReport.Text = sb.ToString();
            _lblReportStatus.Text = $"Report generated in {stopwatch.ElapsedMilliseconds} ms.";
            SetStatus($"Report generated in {stopwatch.ElapsedMilliseconds} ms");
        }
    }

    
    // MODELS

    public class Student
    {
        public string StudentNumber { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
    }

    public class Enrollment
    {
        public string StudentNumber { get; set; } = "";
        public string CourseName { get; set; } = "";
    }

    public class MarkRecord
    {
        public string StudentNumber { get; set; } = "";
        public string CourseName { get; set; } = "";
        public double MarkValue { get; set; }
    }

    public class ComboItem
    {
        public string Value { get; set; }
        public string Display { get; set; }

        public ComboItem(string value, string display)
        {
            Value = value;
            Display = display;
        }
    }
}
