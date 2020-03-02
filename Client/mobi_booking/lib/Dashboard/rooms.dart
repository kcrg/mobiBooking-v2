import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:http/http.dart' as http;

class Rooms extends StatefulWidget {
  _RoomsState createState() => _RoomsState();
}

class _RoomsState extends State<Rooms> {
  
  bool waitForResponse = true;
  String reservated = "0";
  String notReservated = "0";

  void initState() {
    super.initState();
    _getReservated();
    _getNotReservated();
  }

  Future _getReservated() async {
    SharedPreferences.getInstance().then((value) {
      String token = value.getString('token');

      http.get(
        "https://mobibookingwebapi.azurewebsites.net/api/Room/get_reservated",
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
            reservated = res.body;
          });
        }
      });
    });
  }

  Future _getNotReservated() async {
    SharedPreferences.getInstance().then((value) {
      String token = value.getString('token');

      http.get(
        "https://mobibookingwebapi.azurewebsites.net/api/Room/get_not_reservated",
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
            notReservated = res.body;
          });
        }
      });
    });
  }

  Widget _generateColumn(bool left) {
    return Align(
      alignment: Alignment.centerLeft,
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.center,
        children: <Widget>[
          Container(
              margin: EdgeInsets.only(left: 15, top: 30),
              alignment: Alignment.centerLeft,
              child: Text(
                left ? "Aktualnie wolne" : "Aktualnie zajęte",
                style: TextStyle(fontSize: 20, color: Colors.grey),
                textAlign: TextAlign.left,
              )),
          Container(
              alignment: Alignment.center,
              margin: EdgeInsets.only(left: 15, top: 15),
              child: Text(
                left ? notReservated : reservated,
                style: TextStyle(
                    fontSize: 30,
                    color: Color(0xff902fdb),
                    fontWeight: FontWeight.bold),
                textAlign: TextAlign.center,
              )),
        ],
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return waitForResponse
        ? Center(
            child: SizedBox(
            child: new CircularProgressIndicator(
              strokeWidth: 5,
            ),
            height: 50.0,
            width: 50.0,
          ))
        : Column(
      children: <Widget>[
        Align(
          alignment: Alignment.topLeft,
          child: Container(
              margin: EdgeInsets.only(left: 15, top: 15),
              child: Text(
                "Sale",
                style: TextStyle(fontSize: 30, color: Colors.grey),
              )),
        ),
        Row(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            Container(
                margin: EdgeInsets.only(left: 0), child: _generateColumn(true)),
            Container(
                margin: EdgeInsets.only(left: 100),
                child: _generateColumn(false))
          ],
        )
      ],
    );
  }
}
