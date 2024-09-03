using System.ComponentModel;

namespace Defendo_Blue
{
    public enum EventList
    {
        [Description("Başarılı giriş")]
        SuccesfulLogin = 4624,

        [Description("Başarısız giriş")]
        FailedLogin = 4625,

        [Description("Ekran kilitlendi")]
        Unlock = 4800,

        [Description("Ekran kilidi açıldı")]
        Lock = 4801,

        [Description("Kullanıcı oturumunu kapattı")]
        Logoff1 = 4634,

        [Description("Başarılı Logoff")]
        Logoff2 = 4647,

        [Description("Yeni kullanıcı oluşturuldu")]
        NewUserCreated = 4720,

        [Description("Kullanıcı silindi")]
        UserDeleted = 4726,

        [Description("Loglar silindi")]
        LogsCleared = 1102,

        [Description("Kullanıcı aktif hale getirildi")]
        AccountEnabled = 4722,

        [Description("Bir kullanıcı hesabı değiştirildi")]
        AccountChanged = 4738,

        [Description("Bir hesap şifresini sıfırlama girişiminde bulunuldu")]
        AccountPasswordChanged = 4724,

        [Description("Bir kullanıcı hesabı devre dışı bırakıldı")]
        AccountDisabled = 4725,

        [Description("Bir ağ paylaşım nesnesine erişildi")]
        AdministrativeShareAccess = 4725,

        [Description("Windows Güvenlik Duvarı Hizmeti başarıyla başlatıldı")]
        FirewallOn = 5024,

        [Description("Windows Güvenlik Duvarı Hizmeti durduruldu")]
        FirewallOff = 5025,

        [Description("Sisteme bir servis kuruldu")]
        NewWServiceCreated = 4697,

        [Description("Bir ağ paylaşım nesnesine erişildi")]
        AdministrativeShareAccess2 = 5140,

        [Description("Remote disconnected: Bir oturumun bir Window.Station ile bağlantısı kesildi")]
        RemoteDisconnected = 4779
    }

    public enum LoginTypes
    {
        [Description("System only")]
        SystemOnly = 0,

        [Description("Yerel login (Örn: Klavye ile)")]
        YerelLogin = 2,

        [Description("Network Login")]
        NetworkLogin = 3,

        [Description("Batch Login- zamanlanmış görevler için kullanılır")]
        BatchLogin = 4,

        [Description("Windows servis login – (Görünür/Interaktif olmayacaktır)")]
        WinServiceLogin = 5,

        [Description("Kilit ekranını açmak/kapatmak için kimlik bilgileri kullanıldı")]
        KilitEkraniAcmakIcinKullanilanLoginBilgisi = 6,

        [Description("Ağ üzerinden kimlik bilgileri clear text olarak gönderildi")]
        AgdanGelenLoginBilgisi = 7,

        [Description("Şu anki oturumu açan haricinde ”run as” komutuyla (Yönetici vs. olarak çalıştır ) yeni kimlik bilgileri kullanıldı")]
        RunAsOlarakGelenLogin = 8,

        [Description("RDP (Uzak Masaüstü)")]
        RDP = 9,

        [Description("Login kimliği cachten getirildi")]
        CacheLogin = 10,

        [Description("Cachten RDP yapıldı")]
        CacheRDP = 11,

        [Description("Cachten kilit açıldı (oturum zaten açık)")]
        CacheKilidiAcildi = 12
    }
}
