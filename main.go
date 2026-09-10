package main

import (
	"fmt"
	"lab2/mathutil"
)

func main() {
	var a int 
	var b int 
	fmt.Print("enter the value of a : ")
	fmt.Scan(&a)
	fmt.Print("enter the value of b : ")
	fmt.Scan(&b)
 
	fmt.Printf("sum is : %d\n", mathutil.Add(a, b))
}

func PrintStr(s string){
	fmt.Println(s)
	fmt.Println("")



}