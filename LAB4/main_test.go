package main

import (
	"testing"
)

func TestDivide(t *testing.T) {
	result, err := Divide(20, 4)
	if err != nil {
		t.Errorf("expected no error, got: %v", err)
	}
	if result != 5 {
		t.Errorf("expected 5, got: %v", result)
	}

	_, err = Divide(10, 0)
	if err == nil {
		t.Errorf("expected error for division by zero, got nil")
	}
}

func TestSqrt(t *testing.T) {
	result, err := Sqrt(16)
	if err != nil {
		t.Errorf("expected no error, got: %v", err)
	}
	if result != 4 {
		t.Errorf("expected 4, got: %v", result)
	}

	_, err = Sqrt(-4)
	if err == nil {
		t.Errorf("expected error for negative square root, got nil")
	}
}

func TestValidateStudentName(t *testing.T) {
	name, err := ValidateStudentName("Subhrajeet")
	if err != nil {
		t.Errorf("expected no error, got: %v", err)
	}
	if name != "Subhrajeet" {
		t.Errorf("expected 'Subhrajeet', got: %v", name)
	}

	_, err = ValidateStudentName("")
	if err == nil {
		t.Errorf("expected error for empty name, got nil")
	}
}

