# Fable.C3

This is just a minimal implementation of a [Fable](https://fable.io/) interface with [C3](https://c3js.org/).

[ts2fable](https://github.com/fable-compiler/ts2fable) was used to generate a full scope interface and pieces pulled out to make a minimal working version.


## Updating from C3 type definition source

### Pre-requisites
* [Yarn](https://yarnpkg.com/lang/en/)
* [Paket](https://fsprojects.github.io/Paket/)
* [FSharp SDK or similiar](https://fsharp.org/use/windows/)

### Modifying source
Run from commandline
* yarn
* yarn c3fabled
* Update content Fable.C3.fs with code from ScriptConverted.C3 as needed