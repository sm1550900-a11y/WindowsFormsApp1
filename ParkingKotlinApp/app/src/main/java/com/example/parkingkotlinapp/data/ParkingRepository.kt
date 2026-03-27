package com.example.parkingkotlinapp.data

class ParkingRepository {
    private var spots = listOf(
        ParkingSpot("A1", 100, SpotStatus.FREE),
        ParkingSpot("A2", 100, SpotStatus.BUSY),
        ParkingSpot("B1", 80, SpotStatus.BOOKED),
        ParkingSpot("B2", 80, SpotStatus.FREE)
    )

    private val history = mutableListOf<Booking>()

    fun getSpots(): List<ParkingSpot> = spots

    fun getHistory(): List<Booking> = history.toList().reversed()

    fun tryBook(spotId: String): Result<String> {
        val target = spots.firstOrNull { it.id == spotId } ?: return Result.failure(Exception("Место не найдено"))
        if (target.status != SpotStatus.FREE) {
            return Result.failure(Exception("Место недоступно"))
        }

        spots = spots.map {
            if (it.id == spotId) it.copy(status = SpotStatus.BOOKED) else it
        }
        history.add(
            Booking(
                spotId = spotId,
                start = "Сейчас",
                end = "+2 часа",
                cost = target.pricePerHour * 2,
                status = "Забронировано"
            )
        )
        return Result.success("Место $spotId забронировано")
    }
}
