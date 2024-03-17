# ПШЕ GIT TRANSLATOR

Консольное приложение, которое перенаправляет введённые команды в стандартное приложение GIT.

Создано для программистов, которые, забывая переключить раскладку клавиатуры со своего языка на английский, продолжают вводить в консоли команды git.

Приложение распознает введенные команды на локальной раскладке и перенаправляет соответствующие им команды в git.

Например:

```
> пше ыефегы
> Перенаправление команды в git:
> git status
```


## Установка

Для использования необходимо:

- Скачать последнюю версию приложения на [странице релизов](https://github.com/SeiOkami/git-translator/releases)
- Указать каталог распакованного файла в переменной среды PATH (Дополнительные параметры системы)

После этого в консоли можно использовать приложение **ПШЕ**.

## Доработка

Если вам необходимо доработать логику приложения, то для этого можно использовать файл [configuration.json](/GitTranslator/configuration.json)

- `DisplayTextBeforeRedirection` - текст, который необходимо отображать перед перенаправлением команд в git
- `DisplayGitCommandArguments` - стоит ли перед перенаправлением выводить текст команды git
- `UseLayoutSubstitution` - использовать логику замены символов. В таком случае, каждая строка параметров приложения будет пытаться приводиться к командам git, которые перечислены ниже
  - `EnglishLayoutSymbols` - порядок символов на англоязычной клавиатуре
  - `LocalLayoutSymbols` - набор локальных раскладок, где порядок символов совпадает с порядком англоязычной клавиатуры
  - `ReplaceableLayoutWords` - заменяемые команды git. Если во введенной в консоли строке есть слово, которое удалось получить путём замены символов локальных раскладок на англоязычные, то такая команда перенаправляется в git. Остальные перенаправляются без замен.
- `UseReplacementDictionary` - использовать логику замены слов
  - `ReplacementDictionary` - словарь слов, которые необходимо заменять перед перенаправлением в git. Здесь можно указать любые слова и команды на которые они будут заменены.

## Контакты

Предложения и замечания можно регистрировать на странице [issues](https://github.com/SeiOkami/git-translator/issues)  
Дополнительные контакты на [странице автора](https://github.com/SeiOkami)