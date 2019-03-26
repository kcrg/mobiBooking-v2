import 'package:flutter/material.dart';

class EmailCustomInput extends StatelessWidget {
  final String text;
  final TextEditingController controller;

  EmailCustomInput({this.controller, this.text});

  @override
  Widget build(BuildContext context) {
    return new Container(
      child: new TextFormField(
        controller: controller,
        decoration: new InputDecoration(
          labelText: text,
          fillColor: Colors.white,
          border: new OutlineInputBorder(
            borderRadius: new BorderRadius.circular(8),
            borderSide: new BorderSide(),
          ),
        ),
        validator: (val) {
          if (val.length == 0) {
            return "Email cannot be empty";
          } else {
            return null;
          }
        },
        keyboardType: TextInputType.emailAddress,
        style: new TextStyle(
          fontFamily: "Poppins",
        ),
      ),
    );
  }
}
