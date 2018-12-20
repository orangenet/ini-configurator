# IniConfigurator

This application helps developers, technicians configuring single or multiple INI files via a simple desktop interface.
Actually, it enables them to configure specific parameters among a long list and prevent unexpected syntax errors.

## Usage

.Net core 3.0 preview doesn't support single exe release, that's why we created the CMD file (in the sample folder) in order to keep the long list of generated files hidden.

To use this application, we recommand to simply add the files 'IniConfigurator.cmd' and 'configurator.ini' and the folder 'IniConfigurator' to the required project and setup the 'configurator.ini' file.

Above an example of the configurator.ini file

```ini
[SampleSection1]
;Comment about param 1
param1=
;Comment about param 6
param6=default value for param 6
[SampleSection2]
;Comment about param 10
param10=
```
