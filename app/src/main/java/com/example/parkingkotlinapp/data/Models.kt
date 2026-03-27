package com.example.parkingkotlinapp.data

enum class SpotStatus { FREE, BUSY, BOOKED }

data class ParkingSpot(
    val id: String,
    val pricePerHour: Int,
    val status: SpotStatus
)

data class Booking(
    val spotId: String,
    val start: String,
    val end: String,
    val cost: Int,
    val status: String
)

data class UserProfile(
    val name: String = "Иван Петров",
    val email: String = "ivan@example.com"
)
