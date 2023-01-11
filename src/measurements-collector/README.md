# measurements-collector

Python script for collecting RuuviTag data and publishing it to the API.

```
pip install -r requirements.txt
```

Install python stuff needed for ruuvitag.

```
sudo apt-get install python-dev python-psutil bluez bluez-hcidump
```

To list bluetooth devices

```
 hcitool dev
```

## Raspberry installation

Run headless mode

> For headless setup, SSH can be enabled by placing a file named ssh, without any extension, onto the boot partition of the SD card from another computer. When the Pi boots, it looks for the ssh file. If it is found, SSH is enabled and the file is deleted. The content of the file does not matter; it could contain text, or nothing at all.
> If you have loaded Raspberry Pi OS onto a blank SD card, you will have two partitions. The first one, which is the smaller one, is the boot partition. Place the file into this one.

Enable wifi... https://www.raspberrypi.org/documentation/configuration/wireless/wireless-cli.md

Raspberry default user is pi and password raspbrry.

Copy collect_ruuvi_data.py and find_ruuvitags.py for testing to raspberry /home/pi/ruuvitag

Run find_ruuvitags.py to check if ruuvitags are found.

```
python find_ruuvitags.py
```
