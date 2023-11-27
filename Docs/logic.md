# Purpose

Fictoria's logic system is based on a custom declarative language. It should be used to describe a taxonomy of the world and logical expressions over the state of that taxonomy.

# Program

A program in this language is similar in concept to a knowledge base in Prolog, with both a bit more and a bit less.  It's comprised of statements defining types, schemata, facts (and antifacts), and functions.  Statements end with a stop, or period.  Functions have parameters that take arguments, and a body that defines its implementation.

In essence, it's Prolog with a type system and without the more advanced features.

# Types

The language has several built-in types (e.g., `object`, `int`, `bool`, etc.).  New types can be defined, usually extending `object` like so:

```
animal: object.
```

This simply establishes that there's such a thing as an `animal` in our world. Let's get a bit more specific:

```
animal: object.
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

Naturally, types can be instantiated.  Let's assume the following program to start:

```
animal: object.
dog: animal.
cat: animal.
```

And now let's say we have two pets, a `cat` and a `dog`.  They can each be defined by a special fact schema called `instance`:

```
instance(fido, dog).
instance(mittens, cat).
```

More on Schemata later.

Here `fido` is a `dog` instance in our world, and `mittens` is a `cat` instance.  It follows that `fido` is-a `dog`, and that `mittens` is-a `cat`, again taxonomically.

# Schemata

A schema is a structure for defining facts about our world.  Let's assume the following program to start:

```
animal: object.
mammal: animal.
insect: animal.

human: mammal.
dog: mammal.

spider: insect.
dragonfly: insect.
```

Now imagine that we want to define how many legs animals can have.  To establish that this idea is even knowable, we define a schema:

```
legs(a: animal, n: int).
```

This creates a schema named `legs` with two parameters: `a` of type `animal`, and `n` of type `int`.  The paramater names are currently meaningless.  Semantically though, it gives structure to knowledge about how many legs various animals have.

# Facts

Similar to how we can instantiate a type, we can instantiate a schema using fact definitions.  This section assumes the program defined in the Schemata section above.  Let's lay down some new facts for how many legs our `animal`s have:

```
legs(human, 2).
legs(dog, 4).
legs(spider, 8).
legs(dragonfly, 6).
```

Each statement defines a fact of the schema `legs`.

The first argument of each fact's arguments must be an `animal` or one of its child types, because that's the type of the first parameter for the schema `legs`.  It follows that the second parameter must be of type `int`.

Semantically we've defined that a `human` has `2` legs, a `dog` has `4`, so on so forth.


A fact cannot be defined without a schema – but together, facts and schemas give structure to knowledge in our little world.

**Note:** facts can be expressed over instances _or_ over types.  See the Variance section for more details.

# Functions

Functions are used to define logical and arithmetic operations over facts, literals, and other functions.  Their "body" or implementation is called an expression.

Let's start with something simple:

```
double(x: int) = x * 2.
square(x: int) = x * x.
```

As you would imagine, the function `double` takes an `int` and multiplies it by `2`, and `square` does the same but multiplies it by itself.  Both functions' return value is of type `int`.

Evaluating `double(2)` would produce `4`, and evaluating `square(4)` would produce `16`.

Furthermore, functions can be composed in expressions, for example:

```
double(square(2))
```

...when evaluated produces `8`.

Functions are referred to as predicates when they resolve to a `bool` type.

### Searching

A function can search for facts in its program using an expression. Let's assume the following:

```
dog: animal.
instance(fido, dog).
age(d: dog, years: int).

instance(fido, dog).
instance(spot, dog).
instance(rex,  dog).

age(fido, 3).
age(spot, 11).
```

Now we'll now define a predicate:

```
is_three(d: dog) = age(d, 3).
```

If we evaluate the expression `is_three(fido)`, which calls our new function, we get `true`, and for `is_three(spot)` we get false.  The former matched a fact, because `fido` is indeed of `age` value `3`, while the latter did not match any facts because `spot`'s `age` is not `3`.

Searching can also use wild cards, as in:

```
has_age(d: dog) = age(d, _).
```

This will produce `true` for both `fido` and `spot` but not `rex`, because using `_` as an argument will match any value in a fact search.  Since there is no matching fact for `rex`, it produces `false`.

Furthermore, we can add conditions when searching fact arguments:

```
young(d: dog) = age(d, _ < 5).
```

Similar in range to `is_three` from earlier, evaluating `young(fido)` produces `true` while `young(spot)` is `false`.  Because `fido`'s age, according to the facts, is `3` which is less than `5` it matches; unfortunately `spot`'s age doesn't match and therefore doesn't qualify her to be be young in this example predicate.

### Binding

Instead of a wildcard, you can bind a fact search result's arguments to a local variable by prepending `@` to the variable's name in place.

Bindings behave as wildcards when matching, so a naked binding will match anything, and a binding with an expression (such as `@a < 5`) will match any facts which evaluate to `true` for that argument.

This will bind the age in years to variable `a` locally, though it is discarded:

```
young(d: dog) = age(d, @a < 5).
```

Here we've bound variable `a` locally in the scope of function (or predicate) `young` to its matched fact.  In evaluating `young(fido)` the variable `a` would be bound to the literal value `3`, though again it is not used.

### Series

A function's definition can include multiple expressions rather than one, called a series.  A series expression is a composite of one or more expressions separated by a `;` delimiter.

The last expression in a series is what the function's type and value will be when evaluated. For example, assume this program:

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

Let's walk through what evaluating `density(rock)` will do:

1. Searches for facts of the schema `mass` where the first argument matches `t`, which is `rock` in our case, and will match any value for the second argument; when found it binds the second argument of the matched fact to the variable `m`
2. Finds the fact `mass(rock, 10.0)` because `rock` matches the first argument and the second argument will match anything; the second argument `10.0` is then bound to `m`
3. The first expression, `mass(t, @m)`, is now finished, and produced `true` because it matched a fact; the interpreter moves to the second statement next
4. Searches for facts of the schema `volume`, similar to #1 but binding the second argument to `v` instead and with the value `5.0`
5. The second expression is also done now, moving onto the last
6. The expression `m / v` is all that's left, so it performs division with a quotient of `2.0`
7. The last statement in the series resulted in `2.0`, and so the evaluation of `density(rock)` produces `2.0` typed as a `float`

This mechanism allows us to search for facts, extract their values, and compute over them thereafter.  It can be seen as some of the imperative paradigm sneaking in.

### Assignment

While variables can be bound to extracted values from a fact, they can also be assigned directly as the result of an expression.

For example:

```
f(x: int) =
    a = 1;
    b = 2;
    a + b.
```

Would evaluate to the `int` value of `3`.  This allows you to compute values once and reuse them across multiple statements.

# Variance

Schemas support type variance when matching during evaluation of expressions.  By default, the types of parameters for schemas are invariant.  However, we can indicate that covariant and contravariant matches are possible.

Invariance means an argument must directly match a type or be an instance of a type, as all examples up to this point have been.

In any case, let's start with covariance.  It's indicated by a `+` prepended to the type of an argument in a schema.  This is essentially the same as polymorphism.  Take the following program:

```
animal: object.
mammal: animal.
instance(dog, mammal).
instance(bat, mammal).

can_fly(a: +animal).
can_fly(bat).
```

The `+` in front of `animal` in `can_fly`'s definition means that the schema will positively match subtypes of `animal` when searching facts during evaluation.  Although `bat` is an instance of `mammal`, by virtue of the type hierarchy, `mammal` is-a `animal` (a subtype, and the type defined in the schema), thus it will match that fact when evaluated.

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

We have some `bag`s, we can be carrying them, we are in fact carrying `b1`, fairly straight foward.  You'll notice that `thing` is contravariant (indicated by the `-`).

Let's look at how some expressions would evaluate:

* `carrying(b1)` is `true` – unsurprising
* `carrying(b1)` is `false` – also unsurprising
* `carrying(bag)` is `true` – because `b1` is-a bag
* `carrying(thing)` is `true` – because `b1` is-a `thing`
* `carrying(tote_bag)` is `false` – because `b1` is-not-a `tote_bag`

Variance affords the ability to define facts' matching behavior, and unlocks the power of the type system when writing functions and expressions.