using System;
using System.Drawing;
using System.Windows.Forms;

namespace YMH219_KeyEventLogger;

public partial class Form1 : Form
{
    private KeyLoggerManager loggerManager;

    
    private Panel pnlHeader;
    private Label lblTitle;
    private Label lblSecurityNotice;

    private Panel pnlStatus;
    private Label lblStatus;

    private Panel pnlButtons;
    private Button btnStart;
    private Button btnStop;
    private Button btnClear;
    private Button btnSave;
    private CheckBox chkIncludeKeyUp;

    private Panel pnlTestInput;
    private Label lblTestHint;
    private TextBox txtTestInput;

    private ListBox lstLogs;

    private Panel pnlStats;
    private Label lblTotalKeys;
    private Label lblTopKeys;

    public Form1()
    {
        InitializeComponent();
        this.Controls.Clear(); 

        loggerManager = new KeyLoggerManager();

        InitializeCustomUI();

        
        this.KeyPreview = true;
        this.KeyDown += Form1_KeyDown;
        this.KeyUp += Form1_KeyUp;

        UpdateStatusUI();
    }

    private void InitializeCustomUI()
    {
        
        this.Text = "YMH219 - Key Event Logger (Eğitsel Keylogger)";
        this.Size = new Size(880, 640);
        this.MinimumSize = new Size(780, 520);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.BackColor = Color.FromArgb(245, 247, 250);
        this.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);

        
        pnlHeader = new Panel
        {
            Dock = DockStyle.Top,
            Height = 65,
            BackColor = Color.FromArgb(33, 43, 54),
            Padding = new Padding(15, 10, 15, 10)
        };

        lblTitle = new Label
        {
            Text = "KEY EVENT LOGGER (EĞİTSEL KLAVYE KAYDEDİCİ)",
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 12f, FontStyle.Bold),
            Dock = DockStyle.Top,
            Height = 25
        };

        lblSecurityNotice = new Label
        {
            Text = "🔒 Güvenli Eğitsel Kapsam: Yalnızca bu pencere odaktayken tuşlar yakalanır. Global hook veya arka plan izlemesi içermez.",
            ForeColor = Color.FromArgb(174, 189, 202),
            Font = new Font("Segoe UI", 8.5f, FontStyle.Italic),
            Dock = DockStyle.Bottom,
            Height = 20
        };

        pnlHeader.Controls.Add(lblSecurityNotice);
        pnlHeader.Controls.Add(lblTitle);

        
        pnlStatus = new Panel
        {
            Dock = DockStyle.Top,
            Height = 40,
            BackColor = Color.FromArgb(254, 226, 226), 
            Padding = new Padding(15, 8, 15, 8)
        };

        lblStatus = new Label
        {
            Text = "■ KAYIT DURUMU: DURDURULDU (Tuşlar dinlenmiyor)",
            ForeColor = Color.FromArgb(185, 28, 28),
            Font = new Font("Segoe UI", 10f, FontStyle.Bold),
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft
        };
        pnlStatus.Controls.Add(lblStatus);

        
        pnlButtons = new Panel
        {
            Dock = DockStyle.Top,
            Height = 55,
            BackColor = Color.White,
            Padding = new Padding(15, 10, 15, 10)
        };

        btnStart = CreateStyledButton("▶ Başlat", Color.FromArgb(16, 185, 129), 0);
        btnStart.Click += (s, e) => { loggerManager.StartLogging(); UpdateStatusUI(); };

        btnStop = CreateStyledButton("⏹ Durdur", Color.FromArgb(239, 68, 68), 115);
        btnStop.Click += (s, e) => { loggerManager.StopLogging(); UpdateStatusUI(); };

        btnClear = CreateStyledButton("🗑 Temizle", Color.FromArgb(100, 116, 139), 230);
        btnClear.Click += (s, e) => { ClearLogsAction(); };

        btnSave = CreateStyledButton("💾 .TXT Olarak Kaydet", Color.FromArgb(37, 99, 235), 345, 160);
        btnSave.Click += (s, e) => { SaveLogsAction(); };

        chkIncludeKeyUp = new CheckBox
        {
            Text = "KeyUp (Bırakma) Olaylarını da Ekle",
            Location = new Point(520, 17),
            AutoSize = true,
            Checked = false,
            ForeColor = Color.FromArgb(51, 65, 85),
            Font = new Font("Segoe UI", 9f, FontStyle.Regular)
        };

        pnlButtons.Controls.Add(btnStart);
        pnlButtons.Controls.Add(btnStop);
        pnlButtons.Controls.Add(btnClear);
        pnlButtons.Controls.Add(btnSave);
        pnlButtons.Controls.Add(chkIncludeKeyUp);

        
        pnlTestInput = new Panel
        {
            Dock = DockStyle.Top,
            Height = 65,
            BackColor = Color.FromArgb(241, 245, 249),
            Padding = new Padding(15, 8, 15, 8)
        };

        lblTestHint = new Label
        {
            Text = "Deneme Alanı: Form üzerindeyken herhangi bir tuşa basabilir veya bu alana metin yazabilirsiniz:",
            Dock = DockStyle.Top,
            Height = 20,
            ForeColor = Color.FromArgb(71, 85, 105),
            Font = new Font("Segoe UI", 8.5f, FontStyle.Bold)
        };

        txtTestInput = new TextBox
        {
            Dock = DockStyle.Bottom,
            Height = 28,
            Font = new Font("Segoe UI", 10f),
            PlaceholderText = "Buraya yazı yazarak tuşların anlık yakalanışını test edebilirsiniz..."
        };

        pnlTestInput.Controls.Add(lblTestHint);
        pnlTestInput.Controls.Add(txtTestInput);

        
        pnlStats = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 45,
            BackColor = Color.FromArgb(33, 43, 54),
            Padding = new Padding(15, 12, 15, 10)
        };

        lblTotalKeys = new Label
        {
            Text = "📊 Toplam Olay: 0",
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 9f, FontStyle.Bold),
            Dock = DockStyle.Left,
            Width = 180
        };

        lblTopKeys = new Label
        {
            Text = "🔥 En Çok Basılan: -",
            ForeColor = Color.FromArgb(250, 204, 21),
            Font = new Font("Segoe UI", 9f, FontStyle.Bold),
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft
        };

        pnlStats.Controls.Add(lblTopKeys);
        pnlStats.Controls.Add(lblTotalKeys);

        
        lstLogs = new ListBox
        {
            Dock = DockStyle.Fill,
            Font = new Font("Consolas", 10f, FontStyle.Regular),
            BackColor = Color.FromArgb(15, 23, 42),
            ForeColor = Color.FromArgb(226, 232, 240),
            BorderStyle = BorderStyle.None,
            IntegralHeight = false
        };

        
        this.Controls.Add(lstLogs);
        this.Controls.Add(pnlStats);
        this.Controls.Add(pnlTestInput);
        this.Controls.Add(pnlButtons);
        this.Controls.Add(pnlStatus);
        this.Controls.Add(pnlHeader);
    }

    private Button CreateStyledButton(string text, Color bgColor, int x, int width = 105)
    {
        Button btn = new Button
        {
            Text = text,
            Location = new Point(x + 15, 10),
            Size = new Size(width, 34),
            BackColor = bgColor,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand,
            Font = new Font("Segoe UI", 9f, FontStyle.Bold)
        };
        btn.FlatAppearance.BorderSize = 0;
        return btn;
    }

    private void Form1_KeyDown(object? sender, KeyEventArgs e)
    {
        HandleKey(e, "KeyDown");
    }

    private void Form1_KeyUp(object? sender, KeyEventArgs e)
    {
        if (chkIncludeKeyUp.Checked)
        {
            HandleKey(e, "KeyUp");
        }
    }

    private void HandleKey(KeyEventArgs e, string eventType)
    {
        LogEntry? entry = loggerManager.ProcessKeyEvent(e, eventType);
        if (entry != null)
        {
            lstLogs.Items.Add(entry.ToString());
            
            lstLogs.SelectedIndex = lstLogs.Items.Count - 1;

            UpdateStatsUI();
        }
    }

    private void UpdateStatusUI()
    {
        if (loggerManager.IsLoggingActive)
        {
            pnlStatus.BackColor = Color.FromArgb(209, 250, 229); 
            lblStatus.ForeColor = Color.FromArgb(4, 120, 87);    
            lblStatus.Text = "● KAYIT DURUMU: AKTİF (Klavye tuşları kaydediliyor...)";
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }
        else
        {
            pnlStatus.BackColor = Color.FromArgb(254, 226, 226); 
            lblStatus.ForeColor = Color.FromArgb(185, 28, 28);    
            lblStatus.Text = "■ KAYIT DURUMU: DURDURULDU (Dinleme kapalı, başlatmak için 'Başlat'a basın)";
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }
    }

    private void UpdateStatsUI()
    {
        lblTotalKeys.Text = $"📊 Toplam Olay: {loggerManager.TotalKeyEvents}";
        lblTopKeys.Text = $"🔥 En Çok Basılan: {loggerManager.GetTopKeysSummary(3)}";
    }

    private void ClearLogsAction()
    {
        if (lstLogs.Items.Count == 0) return;

        DialogResult result = MessageBox.Show(
            "Ekrandaki tüm kayıtları silmek istediğinize emin misiniz?",
            "Kayıtları Temizle",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            loggerManager.ClearLogs();
            lstLogs.Items.Clear();
            UpdateStatsUI();
        }
    }

    private void SaveLogsAction()
    {
        if (loggerManager.Logs.Count == 0)
        {
            MessageBox.Show(
                "Kaydedilecek herhangi bir tuş olayı bulunamadı. Lütfen önce kayıt yapın.",
                "Uyarı",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        using (SaveFileDialog sfd = new SaveFileDialog())
        {
            sfd.Filter = "Metin Dosyaları (*.txt)|*.txt|Tüm Dosyalar (*.*)|*.*";
            sfd.Title = "Klavye Olay Kayıtlarını Kaydet";
            sfd.FileName = $"KeyEventLog_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    loggerManager.SaveLogsToFile(sfd.FileName);
                    MessageBox.Show(
                        $"Kayıtlar başarıyla kaydedildi!\n\nDosya Yolu:\n{sfd.FileName}",
                        "Başarılı",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Dosya kaydedilirken hata oluştu:\n" + ex.Message,
                        "Hata",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
    }
}