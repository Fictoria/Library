# Purpose

Fictoria's logic system is based on a custom declarative language. It should be used to describe a taxonomy of the world and logical expressions over the state of that taxonomy.

# Program

A program in this language is similar in concept to a knowledge base in Prolog, with both a bit more and a bit less.  It's comprised of statements defining types, schemata, facts (and antifacts), and functions.  Statements end with a stop, or period.  Functions have parameters that take arguments, and a body that defines their implementation.

In essence, it's Prolog with a type system and without the more advanced features.

# Types

The language has several built-in types (e.g., `object`, `int`, `bool`, etc.).  New types can be defined, usually extending `object` like so:

```
animal: object.
```

This simply establishes that there's such a thing as an `animal` in our world. Let's get a bit more specific:

```
dog: animal.
cat: animal.
```

Here, a `dog` is-a `animal`, and a `cat` is-a `animal`, in the taxonimic sense.  We're establishing an extremely rudimentary biological taxonomy with this example.

The type system can go a bit further. We might specify multiple parent types as well:

```
animal: object.
mammal: animal.
insect: animal.

flying: animal.

dog: mammal.
bat: mammal, flying.
dragonfly: insect, flying.
```

Note `flying`, and both `bat` and `dragonfly`.

In an aspect-oriented way, `flying` is a cross-cutting category of `animal`s, meaning we could arrive at `bat` starting from either `mammal` or from `flying` when traversing our taxonomy downward.  This flexibility affords organizing types in a way that isn't strictly a tree but remains a directed acyclic graph.

# Instances

Naturally, types can be instantiated.  Let's start with the following program:

```
animal: object.
dog: animal.
cat: animal.
```

And now we'll say we have two pets, a `cat` and a `dog`.  They need names though.  So let's define each with a special fact schema called `instance`:

```
instance(fido, dog).
instance(mittens, cat).
```

Here `fido` is a `dog` instance in our world (our pet), and `mittens` is a `cat` instance (our other pet).  It follows that `fido` is-a `dog`, and that `mittens` is-a `cat`, again taxonomically.

# Schemata

A schema is a structure for defining facts about our world.  We can assume the following program:

```
animal: object.
mammal: animal.
insect: animal.

human: mammal.
dog: mammal.

spider: insect.
dragonfly: insect.
```

Now imagine that we want to describe how many legs animals can have.  To establish that this idea is even knowable in the first place, we can write:

```
legs(a: animal, n: int).
```

This will create a schema named `legs` with two parameters: `a` of type `animal`, and `n` of type `int`.  The paramater names are currently meaningless; it's the types that matter structurally.  Semantically though, it gives form to knowledge about how many legs various animals have.

# Facts

Similar to how types can be instantiated, we can "instantiate" a schema using fact definitions.  Let's assume the program defined in the Schemata section above.  Now we'll lay down some new facts about how many legs our `animal`s have:

```
legs(human, 2).
legs(dog, 4).
legs(spider, 8).
legs(dragonfly, 6).
```

Each statement defines a fact of the schema `legs`.

The first argument of each fact here must be an `animal` (is-a), because that's the type of the first parameter for the schema `legs`.  It follows that the second argument must be of type `int`.

Semantically we've defined that a `human` has `2` legs, a `dog` has `4`, so on so forth.


**Note:** a fact cannot be defined without a schema – but together, facts and schemas give structure to knowledge in our little world

**Note:** facts can be expressed over instances or over types, or over some combination thereof.  See the Variance section for more

# Functions

Functions are used to define logical and arithmetic operations over facts, literals, and other functions.  Their "body" or implementation, following a `=` is called an expression.

Let's start with something simple:

```
double(x: int) = x * 2.
square(x: int) = x * x.
```

As you would imagine, the function `double` takes an `int` and multiplies it by `2`, and `square` does the same but multiplies it by itself.  Both functions' return value is of type `int`.

Evaluating `double(2)` would produce `4`, and evaluating `square(4)` would produce `16`.

Further, functions can be composed, for example:

```
double(square(2))
```

Produces `8` as an `int`.

Functions are referred to as predicates when they resolve to a `bool` type.

### Searching

A function can search for facts in its program using an expression. Let's look at the following:

```
animal: object.
dog: animal.
instance(fido, dog).
age(d: dog, years: int).

instance(fido, dog).
instance(spot, dog).
instance(rex,  dog).

age(fido, 3).
age(spot, 11).
```

Now we'll now define a function, or predicate:

```
is_three(d: dog) = age(d, 3).
```

If we evaluate the expression `is_three(fido)`, which calls our new function, we get `true`, and for `is_three(spot)` we get false.  The former matched a fact, because `fido` is indeed of `age` value `3`, while the latter did not match any facts because `spot`'s `age` is not `3`.

Searching can also use wild cards to match facts in the program, as in:

```
has_age(d: dog) = age(d, _).
```

This will produce `true` for both `fido` and `spot` but not `rex`, because using `_` as an argument will match any value in a fact search.  Since there is no matching fact for `rex`, it produces `false`.

Furthermore, we can add conditions when searching fact arguments:

```
young(d: dog) = age(d, _ < 5).
```

The function `young` is similar in range to `is_three` from earlier.  Evaluating `young(fido)` produces `true` while `young(spot)` is `false`.  Because `fido`'s age, according to the facts, is `3` which is less than `5`, the fact matches.  Unfortunately `spot`'s age doesn't match (since `11 < 5` is `false`) and therefore doesn't qualify her to be be young in this example predicate.

### Binding

Instead of a wildcard, you can bind a fact search matched result's arguments to a local variable by prepending `@` to the variable's name you'd like to create and bind.  This section assumes the program from the Searching section.

Bindings behave as wildcards when matching, so a naked binding will match anything (just `@x` with comparison), and a binding with an expression (such as `@a < 5`) will match any facts which evaluate to `true` for that argument as an expression.

```
young(d: dog) = age(d, @a < 5).
```

During evaluation, this will bind the age in years to variable `a`, local to the scope of function `young`, for a searched-for-and-found fact with schema `age`, where `years` is less than `5`.  The variable `a` is not used in this specific example, so it's discarded during evaluation.  In evaluating `young(fido)`, the variable `a` will be bound to the literal value `3`.

Searches without a match do not bind variables to anything.

### Series

A function's definition can include multiple expressions rather than one, called a series.  Series expressions are a composite of one or more expressions separated by a semicolon `;` delimiter.

The function's return type and value are that of the last expression in a series.

For example, consider this program:

```
thing: object.
instance(rock, thing).

mass(t: thing, m: float).
volume(t: thing, v: float).

mass(rock, 10.0).
volume(rock, 5.0).

density(t: thing) =
    mass(t, @m);
    volume(t, @v);
    m / v.
```

Note that there are three expressions separated by two semicolons in the definition of the `density` function.  This is a series.

Let's walk through what evaluating `density(rock)` will do:

1. Binds `t` to `rock`
1. Searches for facts of the schema `mass` where the first argument matches `t`, which is `rock` in our case, and will match any value for the second argument; when found it binds the second argument of the matched fact to the variable `m`
1. Finds the fact `mass(rock, 10.0)` because `rock` matches the first argument, and the second argument will match any value; the second argument `10.0` is then bound to `m`
1. The first expression, `mass(t, @m)`, is now finished, and produced `true` because it matched a fact; the interpreter moves to the second statement next
1. Searches for facts of the schema `volume`, similar to #2 and #3 but binds the second argument to `v` instead and with the value `5.0`
1. The second expression is also done now, moving onto the last
1. The expression `m / v` is all that's left, so it performs division with a quotient of `2.0` (since `10.0/5.0` is `2.0`)
1. The last statement in the series resulted in `2.0`, and so the evaluation of `density(rock)` produces `2.0` typed as a `float`
1. That's it – `density(rock)` evaluates to `2.0` and we're done

This mechanism in the language allows us to search for facts, extract their values, and compute over them thereafter.  It can be seen as some of the imperative paradigm sneaking in.

### Assignment

While variables can be bound to extracted values from a fact's arguments, they can also be assigned directly as the result of excaluation of an expression.

For example:

```
f(x: int) =
    a = 1;
    b = 2;
    a + b.
```

This function ignores `x`.  When evaluated, it always returns the `int` value of `3`.  Assignment expressions allow you to compute values once and reuse them in subsequent expressions.

### Tuples

Functions can return tuples of arbitrary length, as their final expression in a series.  For example:

```
f(x: int) =
    y = x * 2;
    z = x / 2;
    [x, y, z].
```

Evaluating `f(4)` produces `[4, 8, 2]`.

# Variance

Schemas support type variance when matching facts during evaluation of expressions (see Searching above).  By default, the types of parameters for schemas are invariant.  However, we can indicate that covariant and contravariant matches are possible.

Invariance means an argument must exactly match a type or be an instance of a type, as all examples up to this point have been.

Let's dive into covariance.  It's indicated by a `+` prepended to the type of a parameter in a schema definition.  Take the following program:

```
animal: object.
mammal: animal.
instance(dog, mammal).
instance(bat, mammal).

can_fly(a: +animal).
can_fly(bat).
```

The `+` in front of `animal` in `can_fly`'s definition means that the schema will positively match subtypes of `animal` when searching facts during evaluation.  This is essentially the same as how polymorphism behaves.  Although `bat` is an instance of `mammal`, by virtue of the type hierarchy, `mammal` is-a `animal` (a subtype, and the type defined in the schema), thus it will match that fact when evaluated.

The inverse is contravariance, indicated by a `-` prepended to a type in a schema definition.  Unintuitively, this will match for _parent_ types, rather than for child types.  Consider this program:

```
thing: object.
bag: thing.
tote_bag: bag.
carrying(b: -thing).

instance(b1, bag).
instance(b2, bag).

carrying(b1).
```

This states that: there's a thing called `bag`s and a specific type of one called `tote_bag`'; we can be carrying any `thing`; we are in fact carrying `b1` which is an instance of `bag`.  You'll notice that `thing` is contravariant in the definition of the `carrying` schema (indicated by the `-`).

Let's look at how some expressions would evaluate against this program:

* `carrying(b1)` is `true` – unsurprising
* `carrying(b1)` is `false` – also unsurprising
* `carrying(bag)` is `true` – because `b1` is-a bag
* `carrying(thing)` is `true` – because `b1` is-a `thing`
* `carrying(tote_bag)` is `false` – because `b1` is-not-a `tote_bag`

Variance affords the ability to define facts' matching behavior, and unlocks the power of the type system when writing functions and expressions.

# Reference

### Expressions

* Can be arbitrarily encapsulated in parentheses

### Built-In Types

These are likely unnecessarily dense.

* nothing
* anything
* bool
* int
* float
* symbol
* object
* schema
* function
* variable
* schema

### Literals

* `true` and `false` – bool
* `0`, `1`, `-42` – int
* `123.0`, `-55.0` – float

### Operators

* Unary
  * `-` - negative numbers
  * `!` – negate a boolean
* Infix
  * `::` – is-a type
  * `+` and `-` – addition and subtraction
  * `*` and `/` – multiply and divide
  * `>`, `<`, `>=`, `<=` – less than, greater than, etc.
  * `==` and `!=` – equals and not equals
  * `and`, `or`, `xor` – boolean equivalents