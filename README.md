# INIF
<div id="top"></div>
<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a>
    <img src="https://uludag.edu.tr/img/swglogolar/inegolIsletmeFak.svg" alt="Logo" width="80" height="80">
  </a>

<h3 align="center">INIF</h3>

  <p align="center">
    Uludağ Üniversitesi İnegöl İşletme Fakültesi'nde kullanılacak olan kütüphane yönetim sistemi uygulamasıdır.
  </p>
</div>


<!-- ABOUT THE PROJECT -->
## Proje Hakkında



## Giriş Yap - Kayıt Ol
<p float="left">
<img width="500" src="https://github.com/emrecanAy/inif-assemsoft/blob/master/images/login.jpg" />
<img width="500" src="https://github.com/emrecanAy/inif-assemsoft/blob/master/images/register.png" />
</p>
Uygulama açıldığında kullancıyı "Giriş Yap" sayfası karşılıyor. Burada kullanıcı e-posta ve şifresini girerek giriş yapıyor. 
Kullanıcı şifresini unuttuğunda "Şifremi Unuttum" özelliğiyle beraber e-posta adresine gönderilen onay kodunu girerek şifresini güncelleyebiliyor. 
Aynı şekilde "Kayıt Ol" sayfasında da kullanıcının girmiş olduğu e-posta adresine bir onay kodu gönderiliyor. Doğru bir şekilde giriş yapıldığında kullanıcı sisteme kaydediliyor.
Buradaki e-posta kontrolleri okulumuz personellerinin sahip olduğu e-posta adreslerinin uzantılarına göre(...@uludag.edu.tr) yapılıyor.

<br/><br/>
<b>Örnek onay kodu e-postası:</b>
<br/>
<img width="420" src="https://github.com/emrecanAy/inif-assemsoft/blob/master/images/verfication-mail.PNG" />

<p align="right">(<a href="#top">Başa dön</a>)</p>

## Gösterge Paneli

<img src="https://github.com/emrecanAy/inif-assemsoft/blob/master/images/dashboard-closed.png" />
Bu panelde kullanıcıya kütüphane hakkındaki bilgiler dinamik olarak gösteriliyor.
Sol menüde ise uygulamadaki diğer panellere erişim sağlanıyor.
Sol üst kısımda o an hangi yönetici giriş yaptıysa onun ad-soyad bilgisi gösteriliyor.
Bu kurmuş olduğumuz oturum yapısıyla uygulamada yapılan her işlemi "hangi yönetici, hangi işlemi, ne zaman yaptı" şeklinde kaydediyoruz.

<p align="right">(<a href="#top">Başa dön</a>)</p>

## Kitap Ekle ve Hızlı Menü

<img src="https://github.com/emrecanAy/inif-assemsoft/blob/master/images/add-book.png" />
<img src="https://github.com/emrecanAy/inif-assemsoft/blob/master/images/add-book-quick-menu.png" />
Bu panelde kütüphaneye dahil edilecek kitapları sisteme ekleme işlemi yapılıyor.
Sağdaki listeden arama yapılarak eklenilecek kitap halihazırda var mı yok mu kontrol edilebiliyor.
Eğer varsa kullanıcı sadece güncelleme işlemi yaparak o kitabın stok sayısını arttırması gerekiyor.
Eğer yoksa da yeni bir kayıt işlemi yapılıyor.

Sağ üstte hızlı menüler bulunuyor. Bu menüler bulunduğumuz arayüzdeki girdiler kaybolmadan dil, yayınevi, yazar gibi çoklu seçim araçlarına istediğimiz değerleri eklememizi sağlıyor.
Örneğin; eklemek istediğimiz kitabın bütün bilgilerini girdik. Fakat o kitabın yazarı çoklu seçim menüsünde bulunmuyor. Önce o yazarı sisteme kaydetmemiz gerekiyor.
Soldaki menüde görülen "Diğer İşlemler" kısmında yazar, yayınevi vb. eklemeler yapılabiliyor. Fakat yazarı eklemek için o arayüze gittiğimizde kitap ekleme arayüzünde girmiş olduğumuz
bütün girdiler kayboluyor. Bu kayıp işlemini önlemek için "Diğer İşlemler" menüsünde yapılan işlemleri hızlı menü olarak kitap ekleme sayfasına da ekledik.

<p align="right">(<a href="#top">Başa dön</a>)</p>

## Kitap Listeleme

<img src="https://github.com/emrecanAy/inif-assemsoft/blob/master/images/list-book.png" />
Bu panelde kütüphanede bulunan kitapları listeleme işlemi yapılıyor.
Kitaplar ad, yazar gibi sahip olduğu niteliklere göre aranabiliyor. Ek olarak bir tarih aralığı belirterek eklenme tarihlerine göre de filtrelenebiliyor. 


<p align="right">(<a href="#top">Başa dön</a>)</p>

## Üye Ekle

<img src="https://github.com/emrecanAy/inif-assemsoft/blob/master/images/add-student.png" />
<img src="https://github.com/emrecanAy/inif-assemsoft/blob/master/images/add-personnel.png" />
Bu panelde kütüphaneye dahil edilecek üyeleri sisteme ekleme işlemi yapılıyor.
Sağdaki listeden arama yapılarak eklenilecek üye halihazırda var mı yok mu kontrol edilebiliyor.
İki farklı üye çeşidi olacağı(Öğrenci-Personel) için ayrı arayüzlerde işlem yapılıyor. 
Kitap ekleme de olduğu gibi burada da çoklu seçimlere ekleme yapabilmek için hızlı menüler bulunuyor.

<p align="right">(<a href="#top">Başa dön</a>)</p>

## Üye Listeleme

<img src="https://github.com/emrecanAy/inif-assemsoft/blob/master/images/list-personnel.png" />
<img src="https://github.com/emrecanAy/inif-assemsoft/blob/master/images/list-student.png" />
Bu panellerde sistemde bulunan üyeleri listeleme işlemi yapılıyor.
Üyeler ad, e-posta gibi sahip olduğu niteliklere göre aranabiliyor. Ek olarak bir tarih aralığı belirterek eklenme tarihlerine göre de filtrelenebiliyor. 


<p align="right">(<a href="#top">Başa dön</a>)</p>

## Kitap Ödünç Verme

<img src="https://github.com/emrecanAy/inif-assemsoft/blob/master/images/deposit-book.png" />
Bu panelde kitaplar üyelere ödünç veriliyor.
Sağ üst kısımdan ödünç verilecek öğrenci, numarasına göre arama yapılarak seçiliyor.
Solda listeden ise hangi kitabı ödünç almak istiyorsa arama yapılarak seçiliyor.
Seçilen öğrencinin ve kitabın detay bilgileri sağdaki kısımda gözüküyor.
Ek olarak sağ alt kısımda seçilen kitabın şu anda hangi üyelerde bulunduğu ve ne zaman teslim edecekleri bilgisi gösteriliyor.
<br/>
<h4>Buradaki ek özellikler şu şekilde:</h4><br/>
-Eğer stokta olmayan bir kitap verilmek istenirse uyarı mesajı gösterilerek bu durum engelleniyor.<br/>
-Aynı üye aynı kitabı tekrar tekrar ödünç alamıyor. Öncelikle ilk aldığını teslim etmesi bekleniyor.<br/>
-Üye seçme kısmındaki çoklu seçim menüsünde öğrenci ve personel olarak iki seçenek bulunuyor. <br/>
Seçilen değere göre öğrenciler veya personeller listeleniyor.<br/>Okul veya sicil numarasına göre arama yapılıyor.

<p align="right">(<a href="#top">Başa dön</a>)</p>

## Kitap Teslim Alma

<img src="https://github.com/emrecanAy/inif-assemsoft/blob/master/images/get-book-back.png" />
Bu panelde ödünç verilen kitaplar üyelerden teslim alınıyor.
Sol kısımdan teslim alınacak öğrenci, numarasına göre arama yapılarak seçiliyor.
Seçilen öğrenciye ödünç verilmiş kitaplar sağ üstte listeleniyor.
Teslim alınacak kitap da sağ üstteki listeden seçiliyor. Seçilen kitabın detay bilgisi getiriliyor. Akabinde teslim al butonuyla teslim alınıyor.

<p align="right">(<a href="#top">Başa dön</a>)</p>

## Sicil Sorgu İşlemleri

<img src="https://github.com/emrecanAy/inif-assemsoft/blob/master/images/check-deposit-book.png" />
Bu panelde hangi üye hangi kitapları almış, hangilerini teslim etmiş veya etmemiş bilgisi görülebiliyor.

<p align="right">(<a href="#top">Başa dön</a>)</p>

## Ödünç Verilen Üye

<img src="https://github.com/emrecanAy/inif-assemsoft/blob/master/images/who-has-now.png" />
Bu panelde soldaki listeden kitap seçilerek o kitabın hangi üyelere ödünç verildiği ve ne zaman teslim edilmesi gerektiği görülebiliyor. 

<p align="right">(<a href="#top">Başa dön</a>)</p>

## Gecikmiş Kitaplar

<img src="https://github.com/emrecanAy/inif-assemsoft/blob/master/images/expired-books.png" />
Bu panelde teslim tarihi geciken kitap ve üye bilgileri listeleniyor. Kaç gün geciktiği vb. bilgilerle destekleniyor.
Tarih aralığına göre filtrelebiliyor.

<p align="right">(<a href="#top">Başa dön</a>)</p>

## Diğer İşlemler

<img src="https://github.com/emrecanAy/inif-assemsoft/blob/master/images/other-ops.png" />
Bu panelde menüdeki detayda göründüğü üzere yazar, kategori, dil vb. içerikleri ekleme, silme ve güncelleme yapılabiliyor.
Tarih aralığına göre filtrelebiliyor.

<p align="right">(<a href="#top">Başa dön</a>)</p>

## Dosya İşlemleri

<img src="https://github.com/emrecanAy/inif-assemsoft/blob/master/images/export-files.png" />
<b>Bu panelde kullanıcı istediği veriyi istediği dosya tipinde çıktı alabiliyor.</b><br/>
<br/>
Örneğin:<br/>
-Tüm Öğrenciler<br/>
-Teslim Tarihi Geçmiş Öğrenciler<br/>
-Tüm Silinmiş Öğrenciler<br/>
-Tüm Kitaplar<br/>
-En Çok Okunan 10 Kitap</br>
-Tüm Silinmiş Kitaplar<br/>
<br/>
<b>Sağdaki kısımda ise üyeye özel çıktılar da alınabiliyor.</b></br>
Örneğin:<br/>
-Ödünç Aldığı Kitaplar<br/>
-Teslim Ettiği Kitaplar<br/>
-Teslim Tarihi Gecikmiş Kitaplar<br/>

<br/>
<br/>
<a href="https://github.com/emrecanAy/inif-assemsoft/tree/master/Exports" target="_blank" >Tüm örnek dosya çıktıları için tıklayın...</a>

<br/>
<br/>
Altta da hangi admin, hangi çıktıyı, ne zaman aldı bilgisi gösteriliyor. Bu caydırıcı yöntemle olası bilgi çalınma işlemlerinin önüne geçiliyor.

<p align="right">(<a href="#top">Başa dön</a>)</p>

## Ayarlar

<img src="https://github.com/emrecanAy/inif-assemsoft/blob/master/images/email-settings.png" />
Burada üyelere gönderilecek e-postalar için temel konfigürasyonlar yapılıyor. 
Üyelere gönderilecek e-posta'ları gönderecek olan temel e-posta adresi ve şifresi belirtiliyor.
Gönderilecek e-posta'nın da içeriği, başlığı ayarlanabiliyor.
Gidecek hatırlatma ve gecikme e-posta'larının günleri de ayarlanabiliyor.

<p align="right">(<a href="#top">Başa dön</a>)</p>

## Ek Özellikler ve Detaylar

-Geçmişe dönük kontroller ve sorgular yapabilmek için hiçbir veri silinmiyor. Aynı zamanda eklenme tarihleri varsayılan olarak tutuluyor.<br/>
-Yöneticiler kaydedilirken parolaları şifrelenerek veritabanına kaydediliyor.<br/>
-Yazmış olduğumuz algoritmayla her gün ödünç verilen kitaplar kontrol edilerek teslim tarihi gecikmiş olan üyelere gecikme e-posta'sı, teslim tarihi yaklaşan üyelere
ise hatırlatma e-posta'sı gönderiliyor.<br/>
-Her yapılan işlem veritabanında kaydediliyor. Bu şekilde hangi işlemin, kim tarafından, ne zaman yapıldığı kontrolünü de yapabiliyoruz.<br/>
-Eğer bir yazar silinmek istenirse ve o yazarın sahip olduğu bir kitap veya kitaplar kütüphanede bulunuyorsa kullanıcıya uyarı mesajı gösteriliyor.
Aynı şekilde tüm diğer "yabancı anahtar" ilişkisi kurulan veriler için bu kontrol geçerli olarak çalışıyor.
<br/><br/>
<b>Örnek gecikme e-postası:</b>
<br/>
<br/>
<img width="400" src="https://github.com/emrecanAy/inif-assemsoft/blob/master/images/expired-mail.PNG" />

<p align="right">(<a href="#top">Başa dön</a>)</p>

## Bilgi

<b>Projemiz her zaman gelişmeye açıktır. Buna olanak sağlamak için projenin arka tarafını mümkün olduğunca profesyonel bir şekilde sektöre ve SOLID, DRY gibi prensiplere uygun kodlamaya çalıştık.
Talebe göre versiyon güncellemeleri gelecektir. 
</b>
<h3>İncelediğiniz için teşekkür ederiz.</h3>

<p align="right">(<a href="#top">Başa dön</a>)</p>


### Teknolojiler

* [.Net](https://docs.microsoft.com/tr-tr/dotnet/welcome)
* [WinForms](https://docs.microsoft.com/tr-tr/dotnet/desktop/winforms/overview/?view=netdesktop-6.0)

<p align="right">(<a href="#top">Başa dön</a>)</p>



<!-- LICENSE -->
## Lisans

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#top">Başa dön</a>)</p>


<!-- CONTACT -->
## İletişim

Emrecan AY (Fullstack Developer) - [@linkedin](https://www.linkedin.com/in/emrecan-ay/) [@instagram](https://www.instagram.com/codemrecan/) - ayemrecan.info@gmail.com  
Şenol Şen (Fullstack Developer) - [@linkedin](https://www.linkedin.com/in/senolsen/) [@github](https://github.com/senolsn) [@instagram](https://www.instagram.com/senols16/) - senoltr@yandex.com  
Melike Yıldız (Front-end Developer) - [@linkedin](https://www.linkedin.com/in/melikeyildiz2/) - yildiz-melike@outlook.com <br/>
Şaban Dönmez (Contributor) - [@linkedin](https://www.linkedin.com/in/%C5%9Faban-d%C3%B6nmez-321572198) [@instagram](https://www.instagram.com/donmezsabann/) - eren.donmez.11@gmail.com <br/>
Ariq Naufal - (Contributor) - [@instagram](https://www.instagram.com/pra_tomoariq/)


Proje Link: [https://github.com/emrecanAy/inif-assemsoft](https://github.com/emrecanAy/inif-assemsoft)

<p align="right">(<a href="#top">Başa dön</a>)</p>
