package main

import "fmt"

func main() {
	var num1, num2 int

	fmt.Println("Simple Integer Calculator")

	fmt.Print("Enter first integer: ")
	fmt.Scan(&num1)

	fmt.Print("Enter second integer: ")
	fmt.Scan(&num2)

	fmt.Printf("%d + %d = %d\n", num1, num2, num1+num2)
	fmt.Printf("%d - %d = %d\n", num1, num2, num1-num2)
	fmt.Printf("%d * %d = %d\n", num1, num2, num1*num2)
}
