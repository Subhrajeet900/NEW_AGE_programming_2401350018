package main

import "fmt"

func main() {
	var num1, num2 int
	var operator string

	fmt.Println("--- Simple Calculator ---")

	fmt.Print("Enter first number: ")
	fmt.Scan(&num1)

	fmt.Print("Enter operator (+, -, *, /): ")
	fmt.Scan(&operator)

	fmt.Print("Enter second number: ")
	fmt.Scan(&num2)

	switch operator {
	case "+":
		fmt.Println("Result:", num1+num2)
	case "-":
		fmt.Println("Result:", num1-num2)
	case "*":
		fmt.Println("Result:", num1*num2)
	case "/":
		fmt.Println("Result:", num1/num2)
	default:
		fmt.Println("Invalid operator")
	}
}
