namespace FSharpTestTypes

type EnumCases = A | B | C

type TypeCases = I of int | B of bool

type MixedCases = A | B | C | I of int | T of int * int | EC of EnumCases | TC of TypeCases | OI of Option<int> | OTC of Option<TypeCases>