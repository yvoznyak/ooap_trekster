# Threat Model для основних флоу проекту Trekster

## 1. Реєстрація користувача

### Загрози:

- Brute Force Attack: Автоматичний підбір паролів для створення або компрометації акаунтів.
- Fake Accounts: Реєстрація великої кількості підроблених акаунтів.
- SQL Injection: Введення небезпечного SQL-коду в поле вводу даних.

### Вирішення:

- Використання CAPTCHA для запобігання автоматичних атак.
- Ліміт на кількість спроб реєстрації з однієї IP-адреси.
- Валідація і фільтрація введених даних (запобігання SQL Injection).
- Політика паролів: мінімальна довжина, символи, цифри тощо.
- Верифікація пошти/телефону для активації облікового запису.

## 2. Авторизація користувача

### Загрози:

- Credential Stuffing: Використання скомпрометованих паролів.
- Phishing: Користувач вводить свої облікові дані на підробленій сторінці.
- Session Hijacking: Перехоплення сесійного токена.
- Man-in-the-Middle (MITM): Перехоплення даних між клієнтом і сервером.

### Вирішення:

- Підтримка двофакторної аутентифікації (2FA).
- Шифрування HTTPS для захисту переданих даних.
- Використання сучасних протоколів для сесій (наприклад, JWT).
- Логування та моніторинг аномальної поведінки.
- Обмеження часу активності сесії.

## 3. Скидання пароля

### Загрози:

- Password Reset Abuse: Зловмисник отримує доступ до електронної пошти користувача.
- Replay Attack: Використання перехопленого токена для скидання пароля.
- Information Disclosure: Витік даних про існування акаунту через некоректні повідомлення.

### Вирішення:

- Токени для скидання пароля повинні бути одноразовими і мати обмежений час дії.
- Відсутність повідомлень про наявність акаунту (наприклад, "Користувача з такою поштою не існує").
- Введення CAPTCHA на сторінці скидання пароля.

## 4. Створення транзакцій (витрати/доходи)

### Загрози:

- Data Tampering: Некоректна зміна даних транзакцій через маніпуляції клієнтським інтерфейсом.
- Cross-Site Scripting (XSS): Введення небезпечного скрипту в поля транзакцій.
- Denial of Service (DoS): Навмисне створення великої кількості транзакцій для перевантаження системи.

### Вирішення:

- Перевірка введених даних на сервері (Server-side validation).
- Захист від XSS через очищення введених даних і впровадження Content Security Policy (CSP).
- Ліміти на кількість транзакцій за певний період часу для одного користувача.

## 5. Перегляд аналітики та звітів

### Загрози:

- Unauthorized Access: Перегляд даних іншого користувача.
- Data Breach: Витік фінансових даних через неправильну конфігурацію доступу.
- Insecure Export: Некоректна обробка запитів на експорт даних.

### Вирішення:

- Авторизація і аутентифікація для перевірки доступу до даних.
- Розмежування прав доступу (RBAC).
- Шифрування даних під час експорту (особливо якщо передаються конфіденційні дані).

## 6. Редагування/видалення транзакцій та рахунків

### Загрози:

- Unauthorized Modification: Зміна даних іншого користувача через уразливості API.
- Race Conditions: Конфлікти при одночасній зміні одного об’єкта кількома запитами.
- Audit Trail Manipulation: Видалення транзакцій без залишення слідів.

### Вирішення:

- Логування всіх змін у базі даних з можливістю їх аудиту.
- Захист API через токени доступу і перевірку прав.
- Використання транзакцій бази даних для уникнення Race Conditions.

## 7. Експорт даних у файли (CSV, Excel)

### Загрози:

- Data Leakage: Збереження файлів у небезпечному місці або передача по незахищених каналах.
- Malicious File Injection: Ін’єкція шкідливих даних у файли для компрометації інших систем.

### Вирішення:

- Експорт файлів повинен бути дозволений тільки авторизованим користувачам.
- Перевірка коректності даних перед експортом.
- Шифрування файлів або передача їх через захищені канали.

## 8. Керування категоріями та налаштуваннями

### Загрози:

- Privilege Escalation: Отримання доступу до категорій іншого користувача.
- Injection Attacks: SQL або Command Injection під час налаштування категорій.

### Вирішення:

- Впровадження перевірки прав доступу на сервері для кожної операції.
- Фільтрація введених даних для захисту від ін’єкцій.
- Обмеження кількості запитів на редагування категорій.

# Загальні заходи безпеки:

## Клієнтська частина:

- Використання HTTPS для всіх з’єднань.
- Валідація введених даних перед надсиланням на сервер.
- Впровадження CSP для захисту від XSS.

## Серверна частина:

- Розмежування прав доступу до різних функцій API.
- Захист від SQL Injection через використання підготовлених запитів.
- Логування всіх дій з високим рівнем деталізації.

## База даних:

- Шифрування даних, особливо конфіденційної інформації.
- Регулярне створення резервних копій.
- Обмеження доступу до бази даних за IP або через VPN.