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

### Encrypting on the command line

```
dnx -p <path to SimpleEncrypt.Encryptor> run -r <region> -k <AWS ID of key> -v <value to encrypt>
```
### Decrypting on the command line

```
dnx -p <path to SimpleEncrypt.Decryptor> run -r <region> -k <AWS ID of key> -v <value to decrypt>
```

### In-process

```
Install-Package SimpleEncrypt
```

#### Encryption

The extension method `Encrypt` (in the `SimpleEncrypt` namespace) works on a
string value to be encrypted. It takes the AWS ID of the key and the region of
the key and returns the encrypted value encoded using Base64.

#### Decryption

The extension method `Decrypt` (in the `SimpleEncrypt` namespace) works on a
value to be decrypted encoded as a Base64 string. It takes the AWS ID of the key
and returns the decrypted value as a string.


## Steps for setting global commands

```
dnu packages add .\SimpleEncrypt\Debug\SimpleEncrypt.1.0.0.nupkg

dnu packages add .\SimpleEncrypt.Encryptor\Debug\SimpleEncrypt.Encryptor.1.0.0.nupkg

dnu packages add .\SimpleEncrypt.Decryptor\Debug\SimpleEncrypt.Decryptor.1.0.0.nupkg

dnu commands install SimpleEncrypt.Encryptor -f c:\users\karthikp\.dnx\packages

dnu commands install SimpleEncrypt.Decryptor -f c:\users\karthikp\.dnx\packages
```

### Uninstall commands

```
dnu commands uninstall simply-encrypt
dnu commands uninstall simply-decrypt
```