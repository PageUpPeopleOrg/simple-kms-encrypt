## Simple KMS Encrypt

A simple C# library and command line tools for encrypting and decrypting values
with Amazon Key Management Service. Allows you to encrypt configuration values
and store them in configuration files while preventing users without access to
the key from access to the plaintext values.

This is primarily useful for a relatively constrained number of individual
configuration items in cases where a more complex setup is not warranted.

This is inspired by [Shush](https://github.com/realestate-com-au/shush) but:

* is implemented in .NET
* provides a client library for in-process decryption
* does not attempt to impose any structure on the value beyond being a UTF-8
string

## Using Simple encrypt in your code

`Install-Package SimpleEncrypt`
or
`dnu install SimpleEncrypt`

### Encryption

The extension method `Encrypt` (in the `SimpleEncrypt` namespace) works on a
string value to be encrypted. It takes the AWS ID of the key and the region of
the key and returns the encrypted value encoded using Base64.

#### Decryption

The extension method `Decrypt` (in the `SimpleEncrypt` namespace) works on a
value to be decrypted encoded as a Base64 string. It takes the AWS ID of the key
and returns the decrypted value as a string.


## Install Simple encrypt command line

### Commands to install the package
```
dnu packages add .\src\artifacts\bin\SimpleEncrypt\Debug\SimpleEncrypt.1.0.0.nupkg
dnu packages add .\src\artifacts\bin\SimpleEncrypt.Console\Debug\SimpleEncrypt.Console.1.0.0.nupkg

dnu commands install SimpleEncrypt.Console -f c:\users\karthikp\.dnx\packages
```

TODO: dnu package add commands to be removed once the code is pushed to nuget.

### Command line usage

encrypt

`sencrypt encrypt -r ap-southeast-1 -k mykey -v myvalue`

decrypt

`sencrypt decrypt -r ap-southeast-1 -v random-encrypted-value`

help

`sencrypt help`

### Commands to uninstall the package

```
dnu commands uninstall sencrypt
```