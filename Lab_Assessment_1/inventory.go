package main

import "fmt"

type Item struct {
	ID       int
	Name     string
	Quantity int
	Price    float64
}

type SummaryStats struct {
	TotalItems int
	TotalValue float64
}

type Inventory struct {
	Items []Item
}

func NewInventory() *Inventory {
	return &Inventory{}
}

func (inv *Inventory) AddItem(item Item) {
	inv.Items = append(inv.Items, item)
}

func (inv *Inventory) UpdateItem(id int, name string, qty int, price float64) {
	for i, it := range inv.Items {
		if it.ID == id {
			inv.Items[i] = Item{id, name, qty, price}
			return
		}
	}
}

func (inv *Inventory) RemoveItem(id int) {
	for i, it := range inv.Items {
		if it.ID == id {
			inv.Items = append(inv.Items[:i], inv.Items[i+1:]...)
			return
		}
	}
}

func (inv *Inventory) ComputeSummaryStats() SummaryStats {
	var total float64
	for _, it := range inv.Items {
		total += float64(it.Quantity) * it.Price
	}
	return SummaryStats{len(inv.Items), total}
}

func main() {
	inv := NewInventory()

	inv.AddItem(Item{1, "Shirts", 10, 50.0})
	inv.AddItem(Item{2, "Pants", 20, 25.0})

	fmt.Println("Initial Items:")
	for _, it := range inv.Items {
		fmt.Printf("ID: %d | Name: %s | Qty: %d | Price: %.2f\n", it.ID, it.Name, it.Quantity, it.Price)
	}

	inv.UpdateItem(2, "Formal Pants", 25, 30.0)
	inv.RemoveItem(1)

	fmt.Println("\nAfter Update and Remove:")
	for _, it := range inv.Items {
		fmt.Printf("ID: %d | Name: %s | Qty: %d | Price: %.2f\n", it.ID, it.Name, it.Quantity, it.Price)
	}

	stats := inv.ComputeSummaryStats()
	fmt.Printf("\nTotal Items: %d\nTotal Value: %.2f\n", stats.TotalItems, stats.TotalValue)
}
