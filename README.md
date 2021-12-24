# Calculator for Elgato Stream Deck

## Features

* Saves result in a text file, which can be imported into OBS
* Configurable number buttons (e.g. 0-9 or multi digit numbers)
* Operations
  * Addition
  * Subtraction

## Installation

Just download the latest file from [Releases](https://github.com/saitho/streamdeck-calculator/releases) and open the file with Stream Deck.

## Configuration

![Example Stream Deck layout](./streamdeck-layout.jpg)

Place as many "Number" buttons as you like and assign a title and a value.
In the example above, I placed 10 for 0-9.

Then also place the operations you need, and the equals (`[=]`) button.

## Usage

If you want to do a calculation, press the operation first, then the number, and then the orange "equals" button.

Example: You want to add 15 to the current result.
Press: `[+]` `[1]` `[5]` `[=]`

Example: You want to subtract 500 from the current result.
Press: `[-]` `[5]` `[0]` `[0]` `[=]`

Pressing `[=]` will update the current result in `%appdata%\Elgato\StreamDeck\Plugins\com.saitho.calculator.sdPlugin\_data\result.txt`.

**Note:** You MUST always start with an operation (i.e. `[+]` or `[-]`).

## Integration with OBS

The text files written by this Stream Deck plugin can be accessed by OBS.

1. Open OBS Studio and add a new "Text (GDI+)" source to your scene.
2. In source properties, check the "Read from file" checkbox between font settings and text input.
3. Select the file at `%appdata%\Elgato\StreamDeck\Plugins\com.saitho.calculator.sdPlugin\_data\result.txt`
4. You're done. Try doing some calculations on your Stream Deck. They should be updated shortly after pressing `[=]`.