# Sensor collector

Python script to collect ruuvitag data and publish it to AWS cloud.

## Configuration

Create and activate python virtual env.

```
python -m venv ./venv
source .venv/bin/activate
```

Install requirements
[AWS IoT Device SDk for Python](https://github.com/aws/aws-iot-device-sdk-python)
[ruuvitag sensor](https://github.com/ttu/ruuvitag-sensor)

```
pip install -r requirements.txt
```

Install python stuff needed for ruuvitag.

```
sudo apt-get install python-dev python-psutil
sudo apt-get install bluez bluez-hcidump
```

To list bluetooth devices

```
 hcitool dev
```

## AWS configuration

See instructions in [AWS SDK](https://github.com/aws/aws-iot-device-sdk-python).

> For the certificate-based mutual authentication connection type. Download the AWS IoT root CA. Use the AWS IoT console to create and download the certificate and private key. You must specify the location of these files when you initialize the client.

In AWS IoT console create and download certificate for thing. Check that these match with the ones in collect_ruuvi_data.py.

Download Amazon Root CA 1 from here https://docs.aws.amazon.com/iot/latest/developerguide/server-authentication.html?icmpid=docs_iot_console#server-authentication-certs

In IoT console attach policy to certificate.

```
rootCAPath
certificatePath
privateKeyPath
```

Required host and clientid you can find from AWS IoT console.

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

Create cron job to periodically call run_ruuvi_collector.sh.

```
crontab -e
```

and add following entry which will run the script every 10 minutes. Make sure collect_ruuvi_data.py is executable.

```
*/10 * * * * /home/pi/ruuvitag/collect_ruuvi_data.py
```
