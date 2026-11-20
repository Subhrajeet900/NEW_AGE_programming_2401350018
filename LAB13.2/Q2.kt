
fun main() {

    // 1. Function with default parameters
    fun greet(name: String = "Student", message: String = "Welcome") {
        println("$message, $name!")
    }

    println("Default Parameters:")
    greet()
    greet("Subhrajeet", "Hello")


    // 2. Function with named arguments
    println("\nNamed Arguments:")
    greet(message = "Good Morning", name = "Rahul")


    // 3. Function with vararg
    fun printNumbers(vararg numbers: Int) {
        println("\nVararg Numbers:")
        for (num in numbers) {
            println(num)
        }
    }

    printNumbers(10, 20, 30, 40, 50)


    // 4. Higher-order function
    fun calculate(numbers: List<Int>, operation: (Int) -> Int) {
        println("\nHigher-Order Function:")
        val result = numbers.map { operation(it) }
        println(result)
    }

    val values = listOf(1, 2, 3, 4, 5)

    calculate(values) { it * it }
}
