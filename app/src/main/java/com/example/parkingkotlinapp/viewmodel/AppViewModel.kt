package com.example.parkingkotlinapp.viewmodel

import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.lifecycle.ViewModel
import com.example.parkingkotlinapp.data.Booking
import com.example.parkingkotlinapp.data.ParkingRepository
import com.example.parkingkotlinapp.data.ParkingSpot
import com.example.parkingkotlinapp.data.UserProfile

class AppViewModel : ViewModel() {
    private val repository = ParkingRepository()

    var email by mutableStateOf("")
    var password by mutableStateOf("")
    var confirmPassword by mutableStateOf("")
    var hint by mutableStateOf("Введите данные для входа или регистрации")
    var bookingMessage by mutableStateOf("Выберите свободное место")
    var notificationsEnabled by mutableStateOf(true)
    var darkTheme by mutableStateOf(false)
    var profile by mutableStateOf(UserProfile())

    var spots by mutableStateOf(repository.getSpots())
        private set

    var history by mutableStateOf(repository.getHistory())
        private set

    fun login(onSuccess: () -> Unit) {
        if (email.isBlank() || password.isBlank()) {
            hint = "Заполните email и пароль"
            return
        }
        onSuccess()
    }

    fun register() {
        hint = if (password == confirmPassword) "Регистрация успешна" else "Пароли не совпадают"
    }

    fun book(spot: ParkingSpot) {
        val result = repository.tryBook(spot.id)
        bookingMessage = result.getOrElse { it.message ?: "Ошибка" }
        spots = repository.getSpots()
        history = repository.getHistory()
    }

    fun updateProfile(name: String, email: String) {
        profile = profile.copy(name = name, email = email)
    }
}
