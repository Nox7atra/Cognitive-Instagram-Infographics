# Cognitive-Instagram-Infographics
##Данная программа позволяет создавать инфографики по профилю инстаграма пользователя
* Для того, чтобы создать инфографику необходимо в классе MainPage.xaml.cs:
* Ввести свой API KEY в API_KEY полученный на сайте [MS Cognitive Sevices Emotion](https://www.microsoft.com/cognitive-services/en-us/emotion-api)
* Ввести имя пользователя Instagram (без @) в  _CurrentProfile.LoadProfileData("INSTAGRAM ACCOUNT NAME", API_KEY);
* Запустить
* Подождать (за прогрессом +- можно следить в Output консольки MSVS)
* Наслаждаться инфографикой

##Краткое описание необходимых классов для использования

###ProfileInfo 
*Класс отвечающий за загрузку и хранение данных о аккаунте*

**public async Task LoadProfileData(string userName, string apiKey, bool isApiKeyTrial = true)** - метод начинающий загрузку данных в профиль. (инициализирует профиль)
  * **userName** - имя пользователя/аккаута(без @)
  * **apiKey** - ключ полученный на сайте [MS Cognitive Sevices Emotion](https://www.microsoft.com/cognitive-services/en-us/emotion-api)
  * **isApiKeyTrial** - параметр отвечающий за то, в каком режиме будет происходить общение с MSCS Emotion API (если true то будет включена задержка не позволяющая делать более 20 запросов в минуту)

###WebInfographicCreator
*Класс отвечающий за создание инфографики*

**public async Task CreateWebInfoGraphic(ProfileInfo info, string templateName)** - метод отвечающий за формирование инфографики по тимплейту (по окончанию работы метода открывает файл с инфографикой)
  * **ProfileInfo info** - данные об аккаунте по которым будет строится инфографика
  * **string templateName** - название файла с веб шаблоном, по которому будет строится инфографика (шаблоны хранятся в Assets/webtemplates/template)

##Пример использования:
```C#
  ProfileInfo profile = new ProfileInfo();
  await profile.LoadProfileData("INSTAGRAM ACCOUNT NAME", "YOUR API KEY");
  WebInfographicCreator creator = new WebInfographicCreator();
  await creator.CreateWebInfoGraphic(profile, "template.html");
```
К статье https://habrahabr.ru/post/314904/

**License**

MIT License

Copyright (c) 2017 Grygory Dyadichenko

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
