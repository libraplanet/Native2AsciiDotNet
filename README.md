# Native2AsciiDotNet

like native2ascii in JDK.

## Usage

```
Native2AsciiDotNet [-reverse] [-encoding encoding_name] [inputfile] [outputfile]

```

### Options

- -reverse
  Converts ASCII to native encoding. By default, the tool converts native encoding to ASCII.

- -encoding encoding_name
  Specifies the character encoding to use. The default is the system's default encoding.

- inputfile
  Specifies the input file to convert. If omitted, the tool reads from standard input.

- outputfile
  Specifies the output file where the result will be saved. If omitted, the tool writes to standard output.

### Examples

```
Native2AsciiDotNet myfile.txt myfile_ascii.txt
Native2AsciiDotNet -reverse myfile_ascii.txt myfile_native.txt
Native2AsciiDotNet -encoding UTF-8 myfile.txt
type myfile_ascii.txt | Native2AsciiDotNet -encoding UTF-8 -reverse > myfile_native.txt
```

## License
MIT License

## Author
libraplanet


## Version
0.0.0.0.0.1


## .NET Framework
v4.0