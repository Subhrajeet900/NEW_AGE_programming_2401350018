package main

import "fmt"

func main() {
	fmt.Println("--- Slice Operations ---")
	
	var name1, name2 string
	fmt.Print("Enter first student name: ")
	fmt.Scanln(&name1)
	fmt.Print("Enter second student name: ")
	fmt.Scanln(&name2)

	students := []string{name1, name2}
	fmt.Println("Initial slice:", students)

	students = append(students, "dash")
	fmt.Println("After adding a student:", students)

	students[1] = "subh"
	fmt.Println("After updating index 1:", students)

	fmt.Println("\n--- map Operations ---")
	
	studentMarks := map[string]int{
		"Math":    90,
		"Science": 85,
	}
	fmt.Println("Initial map:", studentMarks)

	studentMarks["English"] = 88
	fmt.Println("After inserting English:", studentMarks)

	subjectToFind := "Math"
	mark, exists := studentMarks[subjectToFind]
	if exists {
		fmt.Printf("Lookup: Found %s with %d marks\n", subjectToFind, mark)
	} else {
		fmt.Printf("Lookup: %s not found\n", subjectToFind)
	}

	delete(studentMarks, "Science")
	fmt.Println("After deleting Science:", studentMarks)
}
