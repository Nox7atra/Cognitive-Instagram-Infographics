# Cognitive-Instagram-Infographics
##Данная программа позволяет создавать инфографики по профилю инстаграма пользователя
* Для того, чтобы создать инфографику необходимо в классе MainPage.xaml.cs:
* Ввести свой API KEY в API_KEY полученный на сайте [MS Cognitive Sevices Emotion](https://www.microsoft.com/cognitive-services/en-us/emotion-api)
* Ввести имя пользователя Instagram (без @) в  _CurrentProfile.LoadProfileData("INSTAGRAM ACCOUNT NAME", API_KEY);
* Запустить
* Наслаждаться инфографикой

**Краткое описание необходимых классов для использования**

**ProfileInfo** - класс отвечающий за загрузку и хранение данных о аккаунте

**public async Task LoadProfileData(string userName, string apiKey, bool isApiKeyTrial = true)** - метод начинающий загрузку данных в профиль. (инициализирует профиль)
  * **userName** - имя пользователя/аккаута(без @)
  * **apiKey** - ключ полученный на сайте [MS Cognitive Sevices Emotion](https://www.microsoft.com/cognitive-services/en-us/emotion-api)
  * **isApiKeyTrial** - параметр отвечающий за то, в каком режиме будет происходить общение с MSCS Emotion API (если true то будет включена задержка не позволяющая делать более 20 запросов в минуту)

**WebInfographicCreator** - класс отвечающий за создание инфографики.

**public async Task CreateWebInfoGraphic(ProfileInfo info, string templateName)** - метод отвечающий за формирование инфографики по тимплейту (по окончанию работы метода открывает файл с инфографикой)
  * **ProfileInfo info** - данные об аккаунте по которым будет строится инфографика
  * **string templateName** - название файла с веб шаблоном, по которому будет строится инфографика (шаблоны хранятся в Assets/webtemplates/template)

**Пример использования:**
```C#
  ProfileInfo profile = new ProfileInfo();
  await profile.LoadProfileData("INSTAGRAM ACCOUNT NAME", "YOUR API KEY");
  WebInfographicCreator creator = new WebInfographicCreator();
  await creator.CreateWebInfoGraphic(profile, "template.html");
```
