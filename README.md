# Контекст

Для примера взята простая бизнес-логика: сервис расчёта заказа ([OrderService](https://github.com/ArtBi1/SoftArch/blob/main/OrderCalculator.cs)), который:

1. Рассчитывает итоговую стоимость заказа с учётом скидки за объём и промо-кода.
2. Проверяет максимальную скидку.
3. Валидирует входные данные (товары > 0, цена > 0).
4. Умеет вычислять налоги (например НДС 20%) и итоговую сумму.

---

## Задание m
**Пример бизнес-логики:** [OrderCalculator.cs](https://github.com/ArtBi1/SoftArch/blob/main/OrderCalculator.cs)  
**Unit Test для бизнес-логики:** [OrderCalculatorTests.cs](https://github.com/ArtBi1/SoftArch/blob/main/OrderCalculatorTests.cs)

---

## Задание n
**Папка с выполнением:** [Task_N](https://github.com/ArtBi1/SoftArch/tree/main/Task_N)

**Пояснение:**  
E2E цепочка тестирования:  
- **Auth -> Login** — получает токен, сохраняет в переменную.  
- **Orders -> Create Order** — создаёт заказ, сохраняет OrderId.  
- **Orders -> Get Order** — получает созданный заказ, сверяет id и сумму.  
- **Health -> Health Check** — проверяет доступность АПИ.

**Тесты включают:**  
- HTTP ответы 200 и 201.  
- Проверку наличия токена, id заказа и суммы.  
- Автоматическое сохранение переменных для следующего шага.

---

## Задание o
**Формат выполнения:** XLS  
**Папка с выполнением:** [Task_O](https://github.com/ArtBi1/SoftArch/tree/main/Task_O)

**Пояснение:**  
Я использовал сервис расчёта заказа как пример, но в критериях оценивания был указан робот-пылесос.  
Протокол UAT тестирования системы управления роботом-пылесосом также находится в [Task_O](https://github.com/ArtBi1/SoftArch/tree/main/Task_O).  

**Дополнительно:**  
Файлы доступны на Google-Диске: [ссылка](https://drive.google.com/drive/folders/1aq4S8DLPSyliaLoz39sENctf_J4Epg0g) (для данного задания — файлы №1 и №2).

---

## Задание p
**Предложенные тесты для регрессионного тестирования**  
**Формат выполнения:** XLS  
**Папка с выполнением:** [Task_P](https://github.com/ArtBi1/SoftArch/tree/main/Task_P)

**Дополнительно:**  
Файлы доступны на Google-Диске: [ссылка](https://drive.google.com/drive/folders/1aq4S8DLPSyliaLoz39sENctf_J4Epg0g) (для данного задания — файлы №3 и №4).
