import 'package:flutter/material.dart';
import './CustomInputs/custom_input.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';
import 'package:shared_preferences/shared_preferences.dart';

class AddRoom extends StatefulWidget {
  @override
  _AddRoomState createState() => _AddRoomState();
}

class _AddRoomState extends State<AddRoom> {
  final roomName = TextEditingController();
  final location = TextEditingController();
  final numberOfPeople = TextEditingController();
  int _currentAva;
  String token;
  bool waitForResponse = false;
  bool showSucces = false;
  bool showError = false;

  List<DropdownMenuItem<int>> itemsAva;

  void initState() {
    super.initState();
    _getItems();
  }

  _addRoom() async {
    setState(() {
      waitForResponse = true;
      showSucces = false;
      showError = false;
    });

    var map = new Map<String, dynamic>();
    map["roomName"] = roomName.text;
    map["location"] = location.text;
    map["availabilityId"] = _currentAva;
    map["numberOfPeople"] = numberOfPeople.text;

    await http.post(
      "https://mobibookingwebapi.azurewebsites.net/api/Room/create",
      body: json.encode(map),
      headers: {
        "Content-Type": "application/json",
        "Authorization": "bearer " + token
      },
    ).then((dynamic res) {
      setState(() {
        waitForResponse = false;
      });

      if (res.statusCode == 200) {
        setState(() {
          waitForResponse = false;
          showSucces = true;
          showError = false;
        });
      } else {
        setState(() {
          waitForResponse = false;
          showSucces = false;
          showError = true;
        });
      }
    });
  }

  Widget _createMessage() {
    return Opacity(
        opacity: showSucces || showError ? 1.0 : 0.0,
        child: Container(
          margin: EdgeInsets.only(top: 20),
          child: Text(
              showSucces
                  ? "Sala dodana poprawnie"
                  : "Błąd - taka sala już istnieje",
              textAlign: TextAlign.center,
              style: TextStyle(
                  color: showSucces ? Colors.green : Colors.red,
                  fontSize: 20,
                  fontWeight: FontWeight.bold)),
        ));
  }

  void _getItems() {
    setState(() {
      waitForResponse = true;
      showSucces = false;
      showError = false;
    });
    SharedPreferences.getInstance().then((value) {
      token = value.getString('token');

      http.get(
        "https://mobibookingwebapi.azurewebsites.net/api/Room/get_room_availabilities",
        headers: {
          "Content-Type": "application/json",
          "Authorization": "bearer " + token
        },
      ).then((dynamic res) {
        setState(() {
          waitForResponse = false;
        });

        if (res.statusCode == 200) {
          var list = jsonDecode(res.body);

          waitForResponse = false;
          showSucces = false;
          showError = false;

          List<DropdownMenuItem<int>> items = new List();

          list.forEach((item) {
            items.add(new DropdownMenuItem<int>(
                value: item["id"], child: Text(item["name"])));
          });

          setState(() {
            itemsAva = items;
            _currentAva = items.first.value;
          });
        } else {
          setState(() {
            waitForResponse = false;
            showSucces = false;
            showError = true;
          });
        }
      });
    });
  }

  void _itemChanged(int value) {
    setState(() {
      _currentAva = value;
    });
  }

  @override
  Widget build(BuildContext context) {
    return ListView(
      children: <Widget>[
        Container(
            margin: EdgeInsets.only(left: 30, right: 30, top: 20, bottom: 10),
            child: CustomInput(
              controller: roomName,
              text: "Nazwa sali",
            )),
        Container(
            margin: EdgeInsets.only(left: 30, right: 30, top: 10, bottom: 10),
            child: CustomInput(
              controller: location,
              text: 'Lokalizacja',
            )),
        Container(
            margin: EdgeInsets.only(left: 30, right: 30, top: 10, bottom: 10),
            child: CustomInput(
              controller: numberOfPeople,
              text: 'Liczba osób',
            )),
        Container(
            margin: EdgeInsets.only(left: 175, right: 175, top: 0, bottom: 0),
            child: new DropdownButton<int>(
              value: _currentAva,
              items: itemsAva,
              onChanged: _itemChanged,
            )),
        Container(
            margin: EdgeInsets.only(top: 20),
            child: Center(
              child: new RaisedButton(
                padding: const EdgeInsets.all(15.0),
                textColor: Colors.white,
                color: Theme.of(context).primaryColor,
                onPressed: _addRoom,
                child: waitForResponse
                    ? Padding(
                        padding: EdgeInsets.only(left: 30, right: 30),
                        child: SizedBox(
                          child: new CircularProgressIndicator(
                            strokeWidth: 3,
                          ),
                          height: 20.0,
                          width: 20.0,
                        ))
                    : new Text(
                        "Dodaj salę",
                        style: TextStyle(
                            fontWeight: FontWeight.bold, fontSize: 20),
                      ),
              ),
            )),
        _createMessage()
      ],
    );
  }
}
