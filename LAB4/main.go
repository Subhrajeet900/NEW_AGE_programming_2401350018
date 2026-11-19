package main

import (
	"fmt"
	"math"
	"strings"
)

func Divide(a, b float64) (float64, error) {
	if b == 0 {
		return 0, fmt.Errorf("division by zero is not allowed: denominator is %v", b)
	}
	return a / b, nil
}

func Sqrt(x float64) (float64, error) {
	if x < 0 {
		return 0, fmt.Errorf("cannot calculate square root of negative number: %v", x)
	}
	return math.Sqrt(x), nil
}

func ValidateStudentName(name string) (string, error) {
	if strings.TrimSpace(name) == "" {
		return "", fmt.Errorf("student name cannot be empty")
	}
	return name, nil
}

func main() {
	fmt.Println("=== Error Scenario 1: Division ===")
	if result, err := Divide(10, 2); err != nil {
		fmt.Println("Error:", err)
	} else {
		fmt.Println("10 / 2 =", result)
	}

	if _, err := Divide(10, 0); err != nil {
		fmt.Println("Error:", err)
	}

	fmt.Println("\n=== Error Scenario 2: Square Root ===")
	if result, err := Sqrt(25); err != nil {
		fmt.Println("Error:", err)
	} else {
		fmt.Println("sqrt(25) =", result)
	}

	if _, err := Sqrt(-9); err != nil {
		fmt.Println("Error:", err)
	}

	fmt.Println("\n=== Error Scenario 3: Empty Input ===")
	if name, err := ValidateStudentName("Subhrajeet"); err != nil {
		fmt.Println("Error:", err)
	} else {
		fmt.Println("Validated student:", name)
	}

	if _, err := ValidateStudentName("   "); err != nil {
		fmt.Println("Error:", err)
	}
}
