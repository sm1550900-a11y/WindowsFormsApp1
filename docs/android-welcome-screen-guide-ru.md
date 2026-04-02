# Подробная инструкция для новичка: как сверстать 1-й экран ParkEasy в Android Studio (XML)

Ниже — пошаговый путь **с нуля**: создаём проект, добавляем XML-разметку, drawable-ресурсы, подключаем экран к `MainActivity`, запускаем и проверяем.

---

## 0) Что получится

Экран с:
- синим фоном,
- логотипом (квадрат с буквой `P`),
- названием `ParkEasy` и подписью,
- кнопками `Войти` и `Регистрация`,
- блоком «Возможности приложения» с 3 пунктами.

---

## 1) Подготовка

1. Установи Android Studio (лучше последнюю стабильную).
2. При первом запуске установи SDK (обычно Studio предложит автоматически).
3. Создай новый проект:
   - **File → New → New Project**
   - Выбери **Empty Views Activity** (именно Views/XML, не Compose)
   - Language: **Kotlin**
   - Minimum SDK: API 24+ (можно 21+, но 24 удобнее)

---

## 2) Структура файлов, которую мы создадим

В модуле `app/src/main` нужны файлы:

- `res/layout/activity_welcome.xml` — сама разметка экрана
- `res/drawable/bg_primary_blue.xml` — синий фон
- `res/drawable/btn_white_filled.xml` — белая кнопка
- `res/drawable/btn_outline_white.xml` — прозрачная кнопка с белой рамкой
- `res/drawable/icon_tile_bg.xml` — подложка под иконки

> Можно делать и в `activity_main.xml`, но для новичка проще отдельный файл `activity_welcome.xml`.

---

## 3) Создай drawable-файлы

### 3.1 `bg_primary_blue.xml`

`app/src/main/res/drawable/bg_primary_blue.xml`

```xml
<?xml version="1.0" encoding="utf-8"?>
<shape xmlns:android="http://schemas.android.com/apk/res/android" android:shape="rectangle">
    <solid android:color="#2D5CE6" />
</shape>
```

### 3.2 `btn_white_filled.xml`

`app/src/main/res/drawable/btn_white_filled.xml`

```xml
<?xml version="1.0" encoding="utf-8"?>
<shape xmlns:android="http://schemas.android.com/apk/res/android" android:shape="rectangle">
    <corners android:radius="14dp" />
    <solid android:color="#FFFFFFFF" />
</shape>
```

### 3.3 `btn_outline_white.xml`

`app/src/main/res/drawable/btn_outline_white.xml`

```xml
<?xml version="1.0" encoding="utf-8"?>
<shape xmlns:android="http://schemas.android.com/apk/res/android" android:shape="rectangle">
    <corners android:radius="14dp" />
    <solid android:color="#00FFFFFF" />
    <stroke
        android:width="2dp"
        android:color="#CCFFFFFF" />
</shape>
```

### 3.4 `icon_tile_bg.xml`

`app/src/main/res/drawable/icon_tile_bg.xml`

```xml
<?xml version="1.0" encoding="utf-8"?>
<shape xmlns:android="http://schemas.android.com/apk/res/android" android:shape="rectangle">
    <corners android:radius="10dp" />
    <solid android:color="#2DFFFFFF" />
</shape>
```

---

## 4) Создай layout экрана

Создай файл:
`app/src/main/res/layout/activity_welcome.xml`

Вставь:

```xml
<?xml version="1.0" encoding="utf-8"?>
<ScrollView xmlns:android="http://schemas.android.com/apk/res/android"
    android:layout_width="match_parent"
    android:layout_height="match_parent"
    android:background="@drawable/bg_primary_blue"
    android:fillViewport="true">

    <LinearLayout
        android:layout_width="match_parent"
        android:layout_height="wrap_content"
        android:orientation="vertical"
        android:paddingStart="16dp"
        android:paddingTop="56dp"
        android:paddingEnd="16dp"
        android:paddingBottom="24dp">

        <LinearLayout
            android:layout_width="match_parent"
            android:layout_height="wrap_content"
            android:layout_marginTop="48dp"
            android:gravity="center_horizontal"
            android:orientation="vertical">

            <TextView
                android:layout_width="84dp"
                android:layout_height="84dp"
                android:background="@drawable/btn_outline_white"
                android:gravity="center"
                android:text="P"
                android:textColor="#FFFFFFFF"
                android:textSize="48sp"
                android:textStyle="bold" />

            <TextView
                android:layout_width="wrap_content"
                android:layout_height="wrap_content"
                android:layout_marginTop="18dp"
                android:text="ParkEasy"
                android:textColor="#FFFFFFFF"
                android:textSize="36sp"
                android:textStyle="bold" />

            <TextView
                android:layout_width="wrap_content"
                android:layout_height="wrap_content"
                android:layout_marginTop="8dp"
                android:text="Найди своё идеальное парковочное место"
                android:textAlignment="center"
                android:textColor="#B3FFFFFF"
                android:textSize="18sp" />
        </LinearLayout>

        <Button
            android:id="@+id/btnLogin"
            android:layout_width="match_parent"
            android:layout_height="56dp"
            android:layout_marginTop="36dp"
            android:background="@drawable/btn_white_filled"
            android:text="Войти"
            android:textAllCaps="false"
            android:textColor="#3567E7"
            android:textSize="22sp"
            android:textStyle="bold" />

        <Button
            android:id="@+id/btnRegister"
            android:layout_width="match_parent"
            android:layout_height="56dp"
            android:layout_marginTop="14dp"
            android:background="@drawable/btn_outline_white"
            android:text="Регистрация"
            android:textAllCaps="false"
            android:textColor="#FFFFFFFF"
            android:textSize="22sp"
            android:textStyle="bold" />

        <TextView
            android:layout_width="wrap_content"
            android:layout_height="wrap_content"
            android:layout_gravity="center_horizontal"
            android:layout_marginTop="72dp"
            android:text="Возможности приложения"
            android:textColor="#CCFFFFFF"
            android:textSize="20sp"
            android:textStyle="bold" />

        <LinearLayout
            android:layout_width="match_parent"
            android:layout_height="wrap_content"
            android:layout_marginTop="20dp"
            android:gravity="center_vertical"
            android:orientation="horizontal">

            <ImageView
                android:layout_width="36dp"
                android:layout_height="36dp"
                android:background="@drawable/icon_tile_bg"
                android:padding="7dp"
                android:src="@android:drawable/ic_menu_mylocation"
                android:tint="#FFFFFFFF" />

            <TextView
                android:layout_width="0dp"
                android:layout_height="wrap_content"
                android:layout_marginStart="12dp"
                android:layout_weight="1"
                android:text="Поиск свободных парковочных мест в режиме реального времени"
                android:textColor="#FFFFFFFF"
                android:textSize="16sp" />
        </LinearLayout>

        <LinearLayout
            android:layout_width="match_parent"
            android:layout_height="wrap_content"
            android:layout_marginTop="14dp"
            android:gravity="center_vertical"
            android:orientation="horizontal">

            <ImageView
                android:layout_width="36dp"
                android:layout_height="36dp"
                android:background="@drawable/icon_tile_bg"
                android:padding="7dp"
                android:src="@android:drawable/ic_menu_my_calendar"
                android:tint="#FFFFFFFF" />

            <TextView
                android:layout_width="0dp"
                android:layout_height="wrap_content"
                android:layout_marginStart="12dp"
                android:layout_weight="1"
                android:text="Бронирование места на нужное время"
                android:textColor="#FFFFFFFF"
                android:textSize="16sp" />
        </LinearLayout>

        <LinearLayout
            android:layout_width="match_parent"
            android:layout_height="wrap_content"
            android:layout_marginTop="14dp"
            android:gravity="center_vertical"
            android:orientation="horizontal">

            <ImageView
                android:layout_width="36dp"
                android:layout_height="36dp"
                android:background="@drawable/icon_tile_bg"
                android:padding="7dp"
                android:src="@android:drawable/ic_menu_myplaces"
                android:tint="#FFFFFFFF" />

            <TextView
                android:layout_width="0dp"
                android:layout_height="wrap_content"
                android:layout_marginStart="12dp"
                android:layout_weight="1"
                android:text="Управление личной информацией и историей бронирований"
                android:textColor="#FFFFFFFF"
                android:textSize="16sp" />
        </LinearLayout>

    </LinearLayout>
</ScrollView>
```

---

## 5) Подключи экран в `MainActivity`

Открой `app/src/main/java/.../MainActivity.kt` и в `onCreate` поставь наш layout:

```kotlin
override fun onCreate(savedInstanceState: Bundle?) {
    super.onCreate(savedInstanceState)
    setContentView(R.layout.activity_welcome)

    val btnLogin = findViewById<Button>(R.id.btnLogin)
    val btnRegister = findViewById<Button>(R.id.btnRegister)

    btnLogin.setOnClickListener {
        Toast.makeText(this, "Нажато: Войти", Toast.LENGTH_SHORT).show()
    }

    btnRegister.setOnClickListener {
        Toast.makeText(this, "Нажато: Регистрация", Toast.LENGTH_SHORT).show()
    }
}
```

Если Android Studio попросит импорты, добавь:

```kotlin
import android.os.Bundle
import android.widget.Button
import android.widget.Toast
```

---

## 6) Проверь `AndroidManifest.xml`

Убедись, что `MainActivity` — launcher activity.
Обычно там уже всё правильно после создания проекта.

---

## 7) Запуск

1. Создай эмулятор: **Tools → Device Manager → Create Device**.
2. Выбери, например, Pixel + Android 13/14.
3. Нажми **Run ▶**.

Если всё сделано верно — увидишь экран как в макете.

---

## 8) Частые ошибки и как исправить

1. **`resource drawable/... not found`**
   - Проверь имена файлов: должны точно совпадать (нижний регистр, `_` вместо пробелов).

2. **Кнопки выглядят не так**
   - У некоторых тем Material кнопки перерисовываются стилями.
   - Быстрое решение: оставь `android:background` как в примере.

3. **Текст слишком большой/маленький**
   - Отрегулируй `sp` и `marginTop`.
   - На маленьких экранах `ScrollView` не даст контенту обрезаться.

4. **Иконки не совпадают с макетом 1-в-1**
   - Сейчас используются системные `@android:drawable/...`.
   - Для точного повторения добавь свои SVG (Vector Asset).

---

## 9) Как сделать «как на картинке» ещё ближе

- Поменять шрифт (например, Inter / SF Pro аналог) через `res/font`.
- Вынести тексты в `res/values/strings.xml`.
- Добавить свой логотип вместо буквы `P` (PNG/SVG в `drawable`).
- Сделать адаптивные отступы через `dimens.xml`.

---

## 10) Мини-чеклист перед сдачей

- [ ] Все 5 файлов созданы в правильных папках.
- [ ] `setContentView(R.layout.activity_welcome)` стоит в `MainActivity`.
- [ ] Кнопки нажимаются (Toast показывается).
- [ ] Экран запускается без крашей.

---

Если хочешь, в следующем шаге могу дать **вторую версию** этой же страницы на `ConstraintLayout` (более «production-friendly»), или сразу показать как привязать переходы: `Войти -> LoginActivity`, `Регистрация -> RegisterActivity`.
