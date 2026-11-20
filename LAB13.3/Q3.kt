
class Student(
    private var rollNo: Int,
    private var name: String,
    private var course: String,
    private var percentage: Double
) {

    fun acceptData() {
        print("Enter Roll Number: ")
        rollNo = readln().toInt()

        print("Enter Student Name: ")
        name = readln()

        print("Enter Course: ")
        course = readln()

        print("Enter Percentage: ")
        percentage = readln().toDouble()
    }

    fun displayData() {
        println("\n----- Student Details -----")
        println("Roll Number: $rollNo")
        println("Name: $name")
        println("Course: $course")
        println("Percentage: %.2f%%".format(percentage))
    }
}

fun main() {

    val student1 = Student(0, "", "", 0.0)
    val student2 = Student(0, "", "", 0.0)

    println("Enter Details of First Student")
    student1.acceptData()
    student1.displayData()

    println("\nEnter Details of Second Student")
    student2.acceptData()
    student2.displayData()
}

