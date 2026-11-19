package main

import "fmt"

func main() {

	fmt.Println("===== FIRST GO PROGRAM =====")
	fmt.Println("Hello, World!")
	fmt.Println("My first Go program")

	var a int = 20
	var b int = 10

	var x float64 = 15.5
	var y float64 = 5.5

	fmt.Println("\n===== INTEGER OPERATIONS =====")
	fmt.Println("Addition:", a+b)
	fmt.Println("Subtraction:", a-b)
	fmt.Println("Multiplication:", a*b)

	fmt.Println("\n===== FLOATING POINT OPERATIONS =====")
	fmt.Println("Addition:", x+y)
	fmt.Println("Subtraction:", x-y)
	fmt.Println("Multiplication:", x*y)
}
