import 'package:flutter/material.dart';
import '../CustomInputs/custom_input.dart';

class RoomReserv extends StatefulWidget {
  @override
  _RoomReservState createState() => _RoomReservState();
}

class _RoomReservState extends State<RoomReserv> {
  final username = TextEditingController();
  final pass = TextEditingController();
  final repeadPass = TextEditingController();
  final name = TextEditingController();
  final sureName = TextEditingController();
  final email = TextEditingController();
  final userType = TextEditingController();

  

  _addRoom() {

  }

  @override
  Widget build(BuildContext context) {
    return ListView(
      children: <Widget>[
        Container(
            margin: EdgeInsets.only(left: 30, right: 30, top: 20, bottom: 10),
            child: CustomInput(
              controller: username,
              text: "Nazwa sali",
            )),
        Container(
            margin: EdgeInsets.only(left: 30, right: 30, top: 10, bottom: 10),
            child: CustomInput(
              controller: pass,
              text: 'Lokalizacja',
            )),
        Container(
            margin: EdgeInsets.only(left: 30, right: 30, top: 10, bottom: 10),
            child: CustomInput(
              controller: repeadPass,
              text: 'Liczba osób',
            )),
        Container(
            margin: EdgeInsets.only(left: 30, right: 30, top: 10, bottom: 10),
            child: CustomInput(
              controller: name,
              text: 'Dostępność',
            )),
        
        Container(
          margin: EdgeInsets.only(top: 20),
            child: Center(
          child: new RaisedButton(
            padding: const EdgeInsets.all(15.0),
            textColor: Colors.white,
            color: Theme.of(context).primaryColor,
            onPressed: _addRoom,
            child: new Text(
              "Dodaj salę",
              style: TextStyle(fontWeight: FontWeight.bold, fontSize: 20),
            ),
          ),
        ))
      ],
    );
  }
}
