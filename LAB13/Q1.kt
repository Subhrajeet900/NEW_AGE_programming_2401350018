
fun main() {

    println("Hello Kotlin!!!")

    print("\nEnter department code (IT/HR/SALES): ")
    val deptCode = readln().uppercase()

    print("Enter years of experience: ")
    val years = readln().toInt()

    print("Enter employee salary: ")
    val salary = readln().toDouble()

    val bonusPercent = when {
        deptCode == "IT" && years > 5 -> 20.0
        deptCode == "HR" && years > 3 -> 15.0
        deptCode == "SALES" && years > 4 -> 12.0
        else -> 10.0
    }

    val bonusAmount = salary * bonusPercent / 100

    println("Bonus Percentage: $bonusPercent%")
    println("Bonus Amount: %.2f".format(bonusAmount))

    print("\nEnter an integer: ")
    val number = readln().toLong()

    val digits = number.toString().filter { it.isDigit() }

    var sum = 0

    for (digit in digits) {
        sum += digit.digitToInt()
    }

    val average = sum.toDouble() / digits.length

    println("Sum of digits: $sum")
    println("Average of digits: $average")
}
