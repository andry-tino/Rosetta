# Rosetta

[![Build Status](http://antino-enlad.cloudapp.net:8080/job/Rosetta/badge/icon)](http://antino-enlad.cloudapp.net:8080/job/Rosetta/)

Toolset for migrating your codebase from C# to TypeScript.

## Overview
**Status:** Work in progress

_Rosetta_ is a project for converting C# code into [TypeScript](http://www.typescriptlang.org/). I do this by means of project [Roslyn](https://github.com/dotnet/roslyn). _Rosetta_ is also a toolset for providing help while converting your codebase to TypeScript. The overall solution includes the following tools:

- **Rosetta** The transpiler to convert ScriptSharp C# code into TypeScript.
- **Rosetta ScriptSharp Definition Generator** A tool to generate TypeScript definition files out of your ScriptSharp C# codebase.

## How it works
_Rosetta_ is written in C# and performs syntax analysis of C# code in order to convert it into _TypeScript_. There are many applications, however _Rosetta_ is developed with those specific ones as targets:

- Providing a tool for converting c#-to-javascript (like [ScriptSharp](https://github.com/nikhilk/scriptsharp)) codebases into _TypeScript_.
- Providing a tool for converting C# codebases into _TypeScript_.
- Providing tools for migrating C#-based codebases into TypeScript.

The translation works by traversing the C# AST generated via Roslyn and generating output according to syntax structures encountered during the tree walking.

## Requirements
_Rosetta_ can be executed on the following platforms:

- Windows

Current supported development environments are:

- Visual Studio 2015

_Rosetta_ depends on:

- .NET Framework 4.0+.
- Project [Roslyn](https://github.com/dotnet/roslyn).

_Rosetta ScriptSharp Definition Generator_ depends on:

- .NET Framework 4.0+.
- Project [Roslyn](https://github.com/dotnet/roslyn) (definition generation via file).
- Project [Mono.Cecil](https://github.com/jbevain/cecil) (definition generation via assembly).
