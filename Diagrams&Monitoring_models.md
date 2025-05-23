# Trekster

## Data model

### ER діаграма

<p align="center">
	<img src="https://github.com/yvoznyak/Trekster_web/blob/main/docs/images/er.jpg" alt="Use case"/>
</p>

### Опис сутностей

**<a href="https://github.com/yvoznyak/Trekster/blob/main/db/uml.pdf" target="_blank">Посилання</a>**

### Класифікація даних по "data retention policy"

#### Рівень конфіденційності

##### Публічні дані:

- Currencies.name: Назви валют не містять конфіденційної інформації.
- Categories.name: Назви категорій є загальними і неконфіденційними.

##### Внутрішні дані:

- Accounts.name: Імена рахунків можуть бути внутрішньою інформацією організації.
- Transactions.note: Нотатки до транзакцій можуть містити службову інформацію.

##### Конфіденційні дані:

- Transactions.sum: Суми транзакцій вважаються фінансовою інформацією, що потребує захисту.
- StartBalances.sum: Початкові баланси рахунків є конфіденційними даними.

#### Тип даних

##### Персональні дані: Відсутні.

##### Фінансові дані:

- StartBalances.sum
- Transactions.sum

##### Дані про операції:

- Transactions.date
- Transactions.idAccount
- Transactions.idCurrency
- Transactions.idCategory
- Transactions.note

##### Референційні дані:
- Currencies
- Categories
- Accounts

#### Період зберігання

##### Короткострокове зберігання (6-12 місяців):

- Transactions.note: Нотатки можуть бути видалені після короткого періоду, якщо вони не використовуються в аналітиці.

##### Середньострокове зберігання (3-5 років):

- Transactions.date
- Transactions.sum
- StartBalances.sum
- Accounts.name

##### Довгострокове зберігання (5+ років):

- Currencies.name
- Categories.name
- Transactions.idAccount
- Transactions.idCurrency
- Transactions.idCategory

#### Юридичні вимоги

- Транзакції (Transactions): Згідно із фінансовими або податковими вимогами, інформація про транзакції повинна зберігатися щонайменше 5 років.
- Початкові баланси (StartBalances): Можуть зберігатися відповідно до законодавчих вимог, наприклад, 5-7 років.
- Категорії та валюти: Дані про категорії та валюти зазвичай не регулюються законодавством, але можуть зберігатися довгостроково для підтримки історичної звітності.

#### Бізнес-значення

##### Критичні дані:

- StartBalances.sum: Важливі для забезпечення точності фінансової звітності.
- Transactions.sum: Необхідні для аналізу доходів, витрат та прибутковості.

##### Архівні дані:

- Currencies.name: Архівуються для забезпечення зв’язності історичних даних.
- Categories.name: Використовуються для історичної звітності.

#### Рекомендації для впровадження політики зберігання

##### Автоматизація очищення даних:

- Використовувати тригери чи крон-завдання для видалення старих нотаток або даних транзакцій, які перевищили строк зберігання.

##### Архівування:

- Дані з таблиць Currencies та Categories можуть бути переміщені до архіву після їх активного використання.

##### Шифрування конфіденційних даних:

- Забезпечити шифрування колонок із фінансовими даними, такими як Transactions.sum і StartBalances.sum.

##### Логування видалення:

- Створити журнал видалення даних для забезпечення прозорості відповідно до регуляторних вимог.

## Resiliency model

### СID діаграма

<p align="center">
	<img src="https://github.com/yvoznyak/ooap_trekster/blob/main/CID.png"/>
</p>

#### Проблема — Невірний баланс рахунку

Опис категорій причин:

- Люди: Помилки через недосконалі знання або некоректну роботу користувачів (наприклад, неправильний введення даних).
- Процес: Невірно виконані операції або помилки в обчисленнях, які можуть бути викликані проблемами з транзакціями чи категоріями.
- Обладнання: Проблеми з сервером чи іншими компонентами інфраструктури, що можуть вплинути на базу даних.
- Матеріали: Невірно введена інформація про валюту чи транзакції, що можуть призвести до неправильного балансу.

#### RMA діаграма

<p align="center">
	<img src="https://github.com/yvoznyak/ooap_trekster/blob/main/RMA.png"/>
</p>

##### Кроки для аналізу та вирішення:

###### Причини:

- Транзакції можуть бути введені з помилками.
- Невірно введені дані можуть призвести до помилок у розрахунках.

###### Корінні причини:

- Невірно введені дані про категорії та валютні курси.
- Відсутність належних перевірок в системі.

###### Рішення:

- Додати механізм валідації даних для категорій та валютних курсів.
- Перевіряти правильність обчислень під час вставки або оновлення транзакцій.
- Впровадити автоматичні тести для перевірки правильності даних.

## Security model

**<a href="https://github.com/yvoznyak/ooap_trekster/blob/main/Mitigation%20plan.md" target="_blank">Threat model та migitation plan</a>**

## Analytics model
Таблиця основних функціональних метрик для застосунку управління особистими фінансами:

| **Метрика**                               | **Виміри**                                                 | **Зв'язок із функціональними вимогами**                   | **Коментарі**                                          |
|-------------------------------------------|------------------------------------------------------------|----------------------------------------------------------|--------------------------------------------------------|
| **Кількість активних користувачів**       | Час (день, тиждень, місяць), географія, тип пристрою       | Відстеження загальної активності користувачів             | Визначає залученість та утримання користувачів         |
| **Кількість нових реєстрацій**            | Час, джерело трафіку, кампанія                             | Ефективність процесу реєстрації та маркетингових зусиль   | Допомагає оцінити зростання бази користувачів          |
| **Кількість входів у систему**            | Час, тип пристрою, географія                               | Зручність та надійність авторизації                       | Відображає регулярність використання застосунку        |
| **Кількість доданих транзакцій**          | Час, категорія витрат, тип транзакції                      | Основна функціональність додавання фінансових даних       | Показує, наскільки активно користувачі ведуть облік    |
| **Кількість згенерованих/переглянутих звітів**         | Час, тип звіту                                             | Функціональність аналітики та звітності                   | Показує, як користувачі аналізують свої фінанси        |
| **Середня тривалість сесії**              | Час, тип пристрою                                          | Рівень залученості користувачів                           | Відображає інтерес до застосунку                       |
| **Кількість експортованих файлів**        | Час, тип даних, користувач                                        | Функціональність експорту даних та зручність використання                  | Відображає потребу користувачів в експорті даних        |
| **Рівень відтоку користувачів**           | Час                                                        | Утримання користувачів та якість застосунку               | Допомагає визначити проблеми з утриманням              |
| **Кількість доданих рахунків**       | Час, тип рахунку                                          | Додавати рахунки                     | Відображає використання функції управління рахунками                     |
| **Відсоток використання функцій**         | Функції, час                                               | Популярність та корисність різних функцій                 | Допомагає у пріоритизації розвитку функціоналу         |

**Воронки (Funnels):**

1. **Funnel реєстрації та першого використання:**
   - Відвідування сторінки реєстрації
   - Заповнення та подання форми реєстрації
   - Підтвердження електронної пошти
   - Перший вхід у систему
   - Додавання першої транзакції

   *Ця funnel допомагає відстежити конверсію від потенційного користувача до активного користувача, який почав використовувати основний функціонал застосунку.*

2. **Funnel використання аналітики та експорту даних:**
   - Вхід у систему
   - Перехід до історії транзакцій
   - Генерація звіту
   - Експорт даних у файл (.csv)

   *Ця funnel дозволяє оцінити, як користувачі взаємодіють з функціями аналізу та експорту даних, і чи допомагає це їм у управлінні фінансами.*

## Monitoring & Alerting model
**Monitoring**

Таблиця з основними операційними метриками, які будуть зібрані для застосунку:

| **Метрика** | **Виміри** | **Зв'язок з інфраструктурними ресурсами** | **Як збирається метрика** |
|-------------|------------|--------------------------------------------|----------------------------|
| **1. Використання CPU** | % використання, кількість ядер | Віртуальні машини, App Service | Azure Monitor, Application Insights |
| **2. Використання пам'яті** | Обсяг використаної/доступної пам'яті | Віртуальні машини, App Service | Azure Monitor, Application Insights |
| **3. Диск I/O** | Швидкість читання/запису в MB/s | Сховища даних, бази даних | Azure Monitor |
| **4. Мережевий трафік** | Вхідний/вихідний трафік в MB/s | Віртуальні мережі, балансувальники навантаження | Azure Network Watcher |
| **5. Час відповіді застосунку** | Середній, максимальний, мінімальний час у мс | Web App, API | Application Insights |
| **6. Кількість запитів** | Запити в секунду | Web App, API | Application Insights |
| **7. Рівень помилок** | % помилкових запитів | Web App, API | Application Insights |
| **8. Продуктивність запитів до БД** | Час виконання запитів у мс | SQL Database, Cosmos DB | Azure SQL Analytics |
| **9. Кількість активних користувачів** | Унікальні користувачі за період | Застосунок | Application Insights |
| **10. Виключення застосунку** | Типи помилок, частота | Застосунок | Application Insights |
| **11. Затримка мережі** | Затримка у мс між клієнтом і сервером | CDN, мережа | Azure Monitor |
| **12. Коди статусів HTTP** | Розподіл 2xx, 3xx, 4xx, 5xx | Web App, API | Application Insights |
| **13. Стан сервісів Azure** | Події, інциденти, оновлення | Всі ресурси Azure | Azure Service Health |

**Пояснення збору метрик:**

- **Azure Monitor**: Інструмент для збору та аналізу телеметричних даних з ресурсів Azure, включаючи віртуальні машини, мережі та сховища.
- **Application Insights**: Сервіс для моніторингу продуктивності та діагностики застосунків, що дозволяє відстежувати запити, виключення та користувацьку активність.
- **Azure Network Watcher**: Забезпечує моніторинг та діагностику мережевої інфраструктури в Azure.
- **Azure SQL Analytics**: Пропонує детальний аналіз продуктивності та використання ресурсів бази даних.

**Alerting**

Метрики з мінімальними/максимальними допустимими значеннями, типом та критичністю досягнення критичних значень, а також планом дій для найкритичніших:

| **Метрика** | **Допустиме значення** | **Тип оповіщення** | **Критичність** | **План дій (Mitigation Plan)** |
|-------------|------------------------|--------------------|-----------------|-------------------------------|
| **1. Використання CPU** | > 80% протягом 10 хвилин | Попередження | Висока | - Масштабувати ресурси<br>- Оптимізувати код<br>- Перевірити навантаження |
| **2. Використання пам'яті** | > 75% постійно | Попередження | Висока | - Оптимізувати використання пам'яті<br>- Додати пам'ять або масштабувати |
| **3. Рівень помилок** | > 5% запитів з помилками | Попередження | Висока | - Перевірити логи<br>- Виправити критичні помилки |
| **4. Час відповіді застосунку** | > 2000 мс середній | Попередження | Середня | - Оптимізувати код<br>- Перевірити базу даних |
| **5. Продуктивність запитів до БД** | > 1000 мс на запит | Попередження | Середня | - Оптимізувати запити<br>- Перевірити індекси |
| **6. Диск I/O** | > 85% використання | Інформація | Середня | - Розширити дисковий простір<br>- Оптимізувати зберігання |
| **7. Коди статусів HTTP 5xx** | > 1% від загальних запитів | Попередження | Висока | - Перевірити серверні помилки<br>- Виправити проблеми |
| **8. Затримка мережі** | > 150 мс постійно | Інформація | Низька | - Перевірити мережеві підключення<br>- Оптимізувати маршрутизацію |

**Migitation plan для найкритичніших метрик:**

1. **Використання CPU та пам'яті:**

   - **Діагностика:**
     - Використати Azure Monitor для виявлення процесів з високим споживанням ресурсів.
   - **Дії:**
     - Масштабувати віртуальні машини або App Service плани.
     - Зробити оптимізацію коду та алгоритмів застосунку.
     - Впровадити кешування та асинхронну обробку.

2. **Рівень помилок та коди статусів HTTP 5xx:**

   - **Діагностика:**
     - Проаналізувати логи за допомогою Application Insights.
     - Визначити часті причини помилок.
   - **Дії:**
     - Виправити помилки в коді та провести повторне тестування.

3. **Час відповіді застосунку та продуктивність запитів до БД:**

   - **Діагностика:**
     - Використовувати профілювання в Application Insights.
     - Аналізувати тривалість запитів до бази даних.
   - **Дії:**
     - Оптимізувати складні або повільні SQL-запити.
     - Додати необхідні індекси або оптимізувати існуючі.
     - Розглянути можливість використання CDN або додаткових кешів.

**Пояснення щодо типів критичності:**

- **Висока:** Потребує негайної уваги для запобігання серйозним збоям або втратам даних.
- **Середня:** Необхідно вирішити проблему в найближчий час, щоб уникнути погіршення сервісу.
- **Низька:** Моніторинг та планове вирішення без негайних дій.

---
