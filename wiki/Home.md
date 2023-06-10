[![openupm](https://img.shields.io/npm/v/com.atomknack.uknackbasis?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.atomknack.uknackbasis/)

Welcome to the UKnack wiki!

<details><summary>Do backup before using new package</summary>Always do backup before using new package, updating package, etc. Not just this - any package. The backup will save your nerve cells and if you will not need it - even better :)</details>

Install examples in the new empty project, if added examples and then deleted, restart project before adding them again - or Unity can lose script references.

Compilled UKnackBasis UPM package available at: [https://github.com/atomknack/UKnackBasis.git#upm](https://github.com/atomknack/UKnackBasis.git#upm) under MIT license

Require at least Unity version **2022.2** - even if package can be installed on lower versions, it most likely will give a lot of errors and custom picker won't work!
Require and was tested with Input System version 1.5.1.

Documentation about specific aspects of UKnack available under [Attribution-NonCommercial-ShareAlike 4.0 International](https://creativecommons.org/licenses/by-nc-sa/4.0/):

**[Attributes](https://github.com/atomknack/UKnackSource/wiki/Attributes)**

**[Attributes-Picker](https://github.com/atomknack/UKnackSource/wiki/Attributes-Picker)**


## Documentation conventions:

<b>nongeneric</b> in documentation and in code

Many scripts in this library are made with code generation. When generating concrete scripts there is need to name then in one conventional way. Therefore scripts that do same things at scripts with some concrete types, but have no calling parameters, used subtypes, etc... Have word nongeneric in place of type.

<b>%TYPE%</b> 

When there are multiple scripts that essentially do same thing for different types such group can be displayed as %TYPE% 

<b>%CLASS%</b>

Same as %TYPE%, but for class names