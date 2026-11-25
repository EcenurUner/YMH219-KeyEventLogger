# YMH219 - Key Event Logger (Eğitsel Keylogger)

Bu proje, **YMH 219 Nesne Tabanlı Programlama** dersi kapsamında geliştirilmiş, Windows Forms ve C# (.NET 10.0) tabanlı bir eğitsel klavye olay kaydedici (Key Event Logger) masaüstü uygulamasıdır.

---

## 🔒 Kapsam ve Güvenlik Bildirimi
Bu uygulama **kesinlikle kötü amaçlı bir sistem keyloggerı değildir**. Uygulama sadece kendi penceresi odaktayken (`ActiveForm`) oluşan tuş olaylarını yakalar. Global klavye kancası (global hook), arka planda gizli çalışma, otomatik başlama, parola/kimlik bilgisi toplama veya ağ üzerinden veri gönderme gibi işlevler içermez. Tamamen eğitim ve klavye olaylarını anlama amaçlıdır.

---

## 🚀 Proje Amacı ve Açıklaması
Bu uygulamanın amacı; C# Windows Forms üzerinde klavye giriş olaylarının (`KeyDown`, `KeyUp`), olay parametrelerinin (`KeyEventArgs`), nesne tabanlı mimarinin ve dosya akışlarının (`StreamWriter`) pratik olarak nasıl işlendiğini göstermektir.

---

## 🛠️ Kullanılan Teknolojiler
- **Programlama Dili:** C# 13 / .NET 10.0
- **Platform:** Windows Forms (WinForms Desktop)
- **Girdi Olayları:** Form Klavye Yönetimi (`KeyPreview`, `KeyDown`, `KeyUp`)
- **Dosya İşlemleri:** `System.IO` (`StreamWriter`, `SaveFileDialog`)
- **IDE:** Visual Studio 2022 / 2026
- **Versiyon Kontrol:** Git & GitHub

---

## 📦 Kurulum ve Çalıştırma Adımları
1. Bu repository'yi bilgisayarınıza indirin veya klonlayın:
   ```bash
   git clone https://github.com/EcenurUner/YMH219-KeyEventLogger.git
   ```
2. Visual Studio ile `YMH219-KeyEventLogger.sln` çözüm dosyasını açın.
3. Çözümü derleyin (**Build > Build Solution** veya `Ctrl+Shift+B`).
4. **F5** tuşuna basarak uygulamayı çalıştırın.

---

## 🎮 Uygulamanın Temel Özellikleri
- **Kayıt Yönetimi:**
  - **Başlat (▶):** Dinleme ve kaydı aktif hale getirir, durum çubuğu yeşile döner.
  - **Durdur (⏹):** Kaydı dondurur, durum çubuğu kırmızıya döner.
  - **Temizle (🗑):** Kullanıcı onayı alarak ekrandaki ve bellekteki tüm kayıtları sıfırlar.
- **Detaylı Kayıt Formatı:** Her tuş vuruşunda; milisaniyelik zaman damgası (`HH:mm:ss.fff`), olay türü (`KeyDown`/`KeyUp`), tuş adı, sanal tuş kodu ve yardımcı tuşlar (`Ctrl`, `Alt`, `Shift`) listelenir.
- **Dosyaya Kaydetme (.txt):** `SaveFileDialog` penceresi açılarak tüm loglar tarih, özet ve istatistik başlıklarıyla birlikte UTF-8 formatında yerel bir `.txt` dosyasına rapor olarak yazdırılır.
- **İsteğe Bağlı Bonus Geliştirmeler (İstatistik Modülü):**
  - **Toplam Tuş Olay Sayacı:** Kaydedilen toplam tuş adedini gerçek zamanlı gösterir.
  - **Tuş Frekans Analizi:** En sık kullanılan ilk 3 tuşu ve basılma adetlerini dinamik olarak hesaplar ve arayüzde gösterir.

---

## 🏗️ Nesne Tabanlı Tasarım (OOP) Mimarisi
- **`LogEntry.cs`**: Her bir tuş olayını bir veri nesnesi olarak modeller; zaman damgası, tuş adı, kodu ve formatlama metotlarını içerir.
- **`KeyLoggerManager.cs`**: Kayıt durumunu kontrol eden, log listesini yöneten, frekans istatistiklerini hesaplayan ve `StreamWriter` ile dosya kaydetme işlemlerini yürüten servis sınıfıdır.
- **`Form1.cs`**: Modern arayüzü sunan, kullanıcı etkileşimlerini yöneten ve olayları `KeyLoggerManager` nesnesine aktaran form bileşenidir.

---

## 📸 Ekran Görüntüleri

### 1. Kayıt Durduruldu (İlk Başlangıç / Bekleme Hali)
![Kayıt Durduruldu](YMH219-KeyEventLogger/screenshot1.png)

### 2. Kayıt Aktif (Tuşların ve İstatistiklerin Yakalandığı An)
![Kayıt Aktif](YMH219-KeyEventLogger/screenshot2.png)

---

## ⚠️ Bilinen Sınırlamalar / Geliştirme Notları
- Güvenlik ve eğitim sınırları gereği yalnızca uygulama aktif pencereyken tuş kaydı tutulur.
- İsteğe bağlı olarak tarih/saat aralığına göre kayıt filtreleme özelliği ilerleyen versiyonlarda geliştirilebilir.

---

## 👤 Öğrenci Bilgileri
- **Ad Soyad:** Ecenur Üner
- **Öğrenci No:** [250541078]
- **Ders:** Nesne Tabanlı Programlama (YMH 219)
