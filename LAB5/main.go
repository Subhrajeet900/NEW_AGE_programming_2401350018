package main

import "fmt"

type Student struct {
	Name string
	Age  int
}

func updateValue(numPtr *int) {
	*numPtr = 100
}

func (s *Student) ReadData() {
	fmt.Print("Enter Name (single word): ")
	fmt.Scanln(&s.Name)

	fmt.Print("Enter Age: ")
	fmt.Scanln(&s.Age)
}

func main() {
	var num int = 42
	fmt.Println("Address:", &num)

	var ptr *int = &num
	fmt.Println("Value:", *ptr)

	var val int = 50
	fmt.Println("Before:", val)
	updateValue(&val)
	fmt.Println("After:", val)

	student := new(Student)

	student.ReadData()

	fmt.Println("Name:", student.Name)
	fmt.Println("Age:", student.Age)

	student.Age = 22
	fmt.Println("Modified Age:", student.Age)
}
