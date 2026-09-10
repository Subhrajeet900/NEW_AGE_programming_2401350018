package main

import (
	"fmt"
	"lab2/mathutil"
)

func main() {
	fmt.Println("math utilities:")
	str := "Hello World"
	fmt.Printf("Original string: %s\n", str)
	fmt.Printf("Reversed string: %s\n", mathutil.Reverse(str))
	fmt.Printf("Vowel count: %d\n\n", mathutil.CountVowels(str))

	fmt.Println("math utilities:")
	num := 5
	base, exp := 2, 3
	fmt.Printf("Factorial of %d is: %d\n", num, mathutil.Factorial(num))
	fmt.Printf("%d to the power of %d is: %d\n", base, exp, mathutil.Power(base, exp))
}