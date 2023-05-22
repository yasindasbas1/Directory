# Telefon Rehber Servisi

Bu proje bir .net core wep api projesidir.
Bu servisin görevi bir veritabına kişi ekleme,silme ve bilgileri üzerinde değişiklik yapmayı sağlar.
Aynı zamanda rapor servisine istekde bulunp rapor oluşmasını ve bu raporların bilgilerini listeleme servisidir.

## Başlangıç

Projeyi indirdikten sonra visual studio dabir solution oluşturup projeyi solution ekleyebilirsiniz.
Projeyi çalıştırabilmek için;
 - TelephoneDirectory.Data
 - TelephoneDirectory.Core
 projelerini indirmeniz gerekmektedir.

### Gereksinimler

Projeyi çalıştırmak için gereken yazılım veya araçlarınn:

- VisualStudio
- TelephoneDirectory.Data
- TelephoneDirectory.Core
- Microsoft.Extensions.Hosting.WindowsServices(Packages)
- Microsoft.OpenApi(Packages)
- Serilog(Packages)
- Serilog.AspNetCore(Packages)
- Serilog.Sinks.Seq(Packages)
- Swashbuckle.AspNetCore(Packages)


### Kullanım

Projeyi çalıştırdıktan sonra "localhost:32001/swagger/index.html" tarayıcınızda url kısmına yazıp açınız.
Swagger ile api uclarına istek atmaya başlayabilirsiniz.


# Telefon Rehberi Rapor Servisi

Bu proje bir .net core wep api projesidir.
Bu servisin görevi bir haberleşme kanalı kullanarak(rabbitmq) talep geldiğinde isteği kuyruğa ekler(message kısmına oluşan rapor talebini Id'si yazılır) ve içerinde barındırdığı background service kuyruğu dinler.
Kuyrukta istek varsa okur ve rapor oluşturmak için okunan Id'i bilgisini reportService aktarırır.
Aynı zamanda Person servisine istekde bulunp kişi bilgilerini çeker.

## Başlangıç

Projeyi indirdikten sonra visual studio dabir solution oluşturup projeyi solution ekleyebilirsiniz.
Projeyi çalıştırabilmek için;
 - TelephoneDirectory.Data
 - TelephoneDirectory.Core
 projelerini indirmeniz gerekmektedir.

### Gereksinimler

Projeyi çalıştırmak için gereken yazılım veya araçlarınn:

- VisualStudio
- TelephoneDirectory.Data
- TelephoneDirectory.Core
- Microsoft.Extensions.Hosting.WindowsServices(Packages)
- Microsoft.OpenApi(Packages)
- Serilog(Packages)
- Serilog.AspNetCore(Packages)
- Serilog.Sinks.Seq(Packages)
- Swashbuckle.AspNetCore(Packages)
- EPPlus.Core(Packages)
- RabbitMQ.Client(Packages)


### Kullanım

Projeyi çalıştırdıktan sonra "localhost:32002/swagger/index.html" tarayıcınızda url kısmına yazıp açınız.
Swagger ile api uclarına istek atmaya başlayabilirsiniz.

#  TelephoneDirectory.Data

Bu proje bir kütüphane sınıfıdır.
İçerisinde database yapılandırmasını barındırır.
DbContext ve entity sınıfları bu entitiy'lere ait configuraiton'ları tamamı bu Library projesinde oluşturulur.

## Başlangıç

Projeyi indirdikten sonra wep api projenizin bulunduğu solution ' eklenmelidir.

#  TelephoneDirectory.Core

Bu proje bir kütüphane sınıfıdır.
İçerisinde static verileri barındırır.
Wep api projelerinde dönüş tipi olarak tasarlanan Result sınıfı ve enumların tamamı burda oluşturulur.

## Başlangıç

Projeyi indirdikten sonra wep api projenizin bulunduğu solution ' eklenmelidir.

# TelephoneDirectory.XunitTest

Bu TelephoneDirectory.Directory ve TelephoneDirectory.Report projelerinde bulunan fonksiyonların test işlemlerini içerir.

## Proje Açıklaması

Projenin amacı güncellenen fonksiyonlarda hata olup olmadığını anlamaktır.
Fonksiyona bir istekde bulunur ve cevabın ne olması gerektiği bellidir.
Beklene cevap alınıyorsa sorun yok ama beklenen cevap alınmıyorsa fonksiyon güncellemesinde bir hata yapılmıştır.

## Başlangıç

Projeyi indirip diğer projeler ile aynı solition içerine eklemeniz yeterlidir.

### Gereksinimler

- Microsoft.AspNetCore.Mvc.Testing(packages)
- Microsoft.NET.Test.Sdk(packages)

## İletişim

İsim: Yasin
Mail = yasin.dasbas1@gmail.com

---