open System

[<AbstractClass>]
type Shape() =
    abstract member Area : unit -> float

type Circle(radius: float) =
    inherit Shape()

    override this.Area() =
        Math.PI * radius * radius

type Rectangle(width: float, height: float) =
    inherit Shape()

    override this.Area() =
        width * height

let circle = Circle(5.0)
let rectangle = Rectangle(4.0, 6.0)

printfn "Circle Area: %.2f" (circle.Area())
printfn "Rectangle Area: %.2f" (rectangle.Area())

let shapes: Shape list =
    [ circle :> Shape; rectangle :> Shape ]

printfn "\nPolymorphism:"

for shape in shapes do
    printfn "Area: %.2f" (shape.Area())