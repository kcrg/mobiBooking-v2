import 'package:flutter/material.dart';

class MyMeetings extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    Widget _generateColumn(bool left) {
      return Align(
        alignment: Alignment.centerLeft,
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            Container(
                margin: EdgeInsets.only(left: 15, top: 30),
                alignment: Alignment.centerLeft,
                child: Text(
                  left ? "Ten tydzień" : "poprz. tydzień",
                  style: TextStyle(fontSize: 20, color: Colors.grey),
                  textAlign: TextAlign.left,
                )),
            Container(
                alignment: Alignment.centerLeft,
                margin: EdgeInsets.only(left: 15, top: 15),
                child: Text(
                  "0.00h",
                  style: TextStyle(
                      fontSize: 30,
                      color: Color(0xff902fdb),
                      fontWeight: FontWeight.bold),
                )),
            Container(
                alignment: Alignment.centerLeft,
                margin: EdgeInsets.only(left: 15, top: 15),
                child: Text(
                  left ? "ten miesiąc" : "poprz. miesiąc",
                  style: TextStyle(fontSize: 20, color: Colors.grey),
                )),
            Container(
                alignment: Alignment.centerLeft,
                margin: EdgeInsets.only(left: 15, top: 15),
                child: Text(
                  "0.00h",
                  style: TextStyle(
                      fontSize: 30,
                      color: Color(0xff902fdb),
                      fontWeight: FontWeight.bold),
                )),
          ],
        ),
      );
    }

    return Column(
      children: <Widget>[
        Align(
          alignment: Alignment.topLeft,
          child: Container(
              margin: EdgeInsets.only(left: 15, top: 15),
              child: Text(
                "Moje spotkania",
                style: TextStyle(fontSize: 30, color: Colors.grey),
              )),
        ),
        Row(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            Container(
                margin: EdgeInsets.only(left: 0),
                child: _generateColumn(true)),
            Container(
                margin: EdgeInsets.only(left: 100),
                child: _generateColumn(false))
          ],
        )
      ],
    );
  }
}
