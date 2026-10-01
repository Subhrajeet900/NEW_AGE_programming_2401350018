package mathutil

func Reverse(s string) string {
	var reversed string
	for i := len(s) - 1; i >= 0; i-- {
		reversed = reversed + string(s[i])
	}
	return reversed
}

func CountVowels(s string) int {
	c := 0
	for i := 0; i < len(s); i++ {
		ch := s[i]
		if ch == 'a' || ch == 'e' || ch == 'i' || ch == 'o' || ch == 'u' ||
			ch == 'A' || ch == 'E' || ch == 'I' || ch == 'O' || ch == 'U' {
			c = c + 1
		}
	}
	return c
}

func Factorial(n int) int {
	ans := 1
	for i := 1; i <= n; i++ {
		ans = ans * i
	}
	return ans
}

func Power(base int, exp int) int {
	ans := 1
	for i := 1; i <= exp; i++ {
		ans = ans * base
	}
	return ans
}