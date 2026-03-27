package com.example.parkingkotlinapp

import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.compose.foundation.background
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.width
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material3.Button
import androidx.compose.material3.Card
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.NavigationBar
import androidx.compose.material3.NavigationBarItem
import androidx.compose.material3.OutlinedTextField
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Switch
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.unit.dp
import androidx.lifecycle.viewmodel.compose.viewModel
import androidx.navigation.NavHostController
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.currentBackStackEntryAsState
import androidx.navigation.compose.rememberNavController
import com.example.parkingkotlinapp.data.ParkingSpot
import com.example.parkingkotlinapp.data.SpotStatus
import com.example.parkingkotlinapp.viewmodel.AppViewModel

class MainActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContent {
            MaterialTheme {
                ParkingApp()
            }
        }
    }
}

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun ParkingApp(vm: AppViewModel = viewModel()) {
    val navController = rememberNavController()

    Scaffold(
        bottomBar = { BottomNavBar(navController) }
    ) { padding ->
        NavHost(
            navController = navController,
            startDestination = "welcome",
            modifier = Modifier.padding(padding)
        ) {
            composable("welcome") { WelcomeScreen(navController) }
            composable("auth") { AuthScreen(vm, navController) }
            composable("main") { MainScreen(vm) }
            composable("history") { HistoryScreen(vm) }
            composable("profile") { ProfileScreen(vm) }
            composable("settings") { SettingsScreen(vm) }
        }
    }
}

@Composable
private fun BottomNavBar(navController: NavHostController) {
    val items = listOf("main" to "Главная", "history" to "История", "profile" to "Профиль", "settings" to "Настройки")
    val navBackStackEntry by navController.currentBackStackEntryAsState()
    val current = navBackStackEntry?.destination?.route

    NavigationBar {
        items.forEach { (route, title) ->
            NavigationBarItem(
                selected = current == route,
                onClick = { navController.navigate(route) },
                icon = { Text("•") },
                label = { Text(title) }
            )
        }
    }
}

@Composable
fun WelcomeScreen(navController: NavHostController) {
    Column(
        modifier = Modifier.fillMaxSize().padding(24.dp),
        verticalArrangement = Arrangement.Center,
        horizontalAlignment = Alignment.CenterHorizontally
    ) {
        Text("🚗 Parking Kotlin App", style = MaterialTheme.typography.headlineMedium)
        Spacer(Modifier.height(12.dp))
        Text("Поиск свободных мест, бронирование и управление профилем")
        Spacer(Modifier.height(20.dp))
        Button(onClick = { navController.navigate("auth") }, modifier = Modifier.fillMaxWidth()) { Text("Войти") }
        Spacer(Modifier.height(8.dp))
        Button(onClick = { navController.navigate("auth") }, modifier = Modifier.fillMaxWidth()) { Text("Регистрация") }
    }
}

@Composable
fun AuthScreen(vm: AppViewModel, navController: NavHostController) {
    Column(modifier = Modifier.fillMaxSize().padding(24.dp), verticalArrangement = Arrangement.spacedBy(10.dp)) {
        OutlinedTextField(value = vm.email, onValueChange = { vm.email = it }, modifier = Modifier.fillMaxWidth(), label = { Text("Email") })
        OutlinedTextField(value = vm.password, onValueChange = { vm.password = it }, modifier = Modifier.fillMaxWidth(), label = { Text("Пароль") })
        OutlinedTextField(value = vm.confirmPassword, onValueChange = { vm.confirmPassword = it }, modifier = Modifier.fillMaxWidth(), label = { Text("Подтвердите пароль") })

        Button(onClick = vm::register, modifier = Modifier.fillMaxWidth()) { Text("Зарегистрироваться") }
        Button(onClick = { vm.login { navController.navigate("main") } }, modifier = Modifier.fillMaxWidth()) { Text("Войти") }
        Text(vm.hint)
    }
}

@Composable
fun MainScreen(vm: AppViewModel) {
    Column(modifier = Modifier.fillMaxSize().padding(16.dp)) {
        Text("Карта парковки", style = MaterialTheme.typography.headlineSmall)
        Spacer(Modifier.height(8.dp))
        LazyColumn(verticalArrangement = Arrangement.spacedBy(8.dp)) {
            items(vm.spots) { spot -> SpotCard(spot, onBook = { vm.book(spot) }) }
        }
        Spacer(Modifier.height(8.dp))
        Text(vm.bookingMessage)
    }
}

@Composable
private fun SpotCard(spot: ParkingSpot, onBook: () -> Unit) {
    val statusColor = when (spot.status) {
        SpotStatus.FREE -> Color(0xFF28A745)
        SpotStatus.BUSY -> Color(0xFFD9534F)
        SpotStatus.BOOKED -> Color(0xFFF39C12)
    }

    Card(modifier = Modifier.fillMaxWidth()) {
        Row(
            modifier = Modifier.fillMaxWidth().padding(12.dp),
            verticalAlignment = Alignment.CenterVertically,
            horizontalArrangement = Arrangement.SpaceBetween
        ) {
            Column {
                Text("Место ${spot.id}")
                Text("${spot.pricePerHour} ₽/ч")
            }
            Text(
                text = "Бронировать",
                color = Color.White,
                modifier = Modifier
                    .background(statusColor)
                    .padding(horizontal = 10.dp, vertical = 6.dp)
                    .clickable { onBook() }
            )
        }
    }
}

@Composable
fun HistoryScreen(vm: AppViewModel) {
    LazyColumn(modifier = Modifier.fillMaxSize().padding(16.dp), verticalArrangement = Arrangement.spacedBy(8.dp)) {
        items(vm.history) { booking ->
            Card(modifier = Modifier.fillMaxWidth()) {
                Column(modifier = Modifier.padding(12.dp)) {
                    Text("Место: ${booking.spotId}")
                    Text("Время: ${booking.start} - ${booking.end}")
                    Text("Стоимость: ${booking.cost} ₽")
                    Text("Статус: ${booking.status}")
                }
            }
        }
    }
}

@Composable
fun ProfileScreen(vm: AppViewModel) {
    var name by remember { mutableStateOf(vm.profile.name) }
    var email by remember { mutableStateOf(vm.profile.email) }

    Column(modifier = Modifier.fillMaxSize().padding(24.dp), verticalArrangement = Arrangement.spacedBy(10.dp)) {
        Text("Личный кабинет", style = MaterialTheme.typography.headlineSmall)
        OutlinedTextField(value = name, onValueChange = { name = it }, modifier = Modifier.fillMaxWidth(), label = { Text("Имя") })
        OutlinedTextField(value = email, onValueChange = { email = it }, modifier = Modifier.fillMaxWidth(), label = { Text("Email") })
        Button(onClick = { vm.updateProfile(name, email) }) { Text("Сохранить") }
    }
}

@Composable
fun SettingsScreen(vm: AppViewModel) {
    Column(modifier = Modifier.fillMaxSize().padding(24.dp), verticalArrangement = Arrangement.spacedBy(12.dp)) {
        Text("Настройки", style = MaterialTheme.typography.headlineSmall)
        Row(verticalAlignment = Alignment.CenterVertically) {
            Text("Уведомления")
            Spacer(Modifier.width(10.dp))
            Switch(checked = vm.notificationsEnabled, onCheckedChange = { vm.notificationsEnabled = it })
        }
        Row(verticalAlignment = Alignment.CenterVertically) {
            Text("Тёмная тема")
            Spacer(Modifier.width(10.dp))
            Switch(checked = vm.darkTheme, onCheckedChange = { vm.darkTheme = it })
        }
    }
}
