import 'package:flutter/material.dart';

class TodayMeetings extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    Widget _generateElement(int index, String time, String title, String room) {
      return Row(
          crossAxisAlignment: CrossAxisAlignment.center,
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: <Widget>[
            Text(time,
                style: TextStyle(
                    color: Theme.of(context).accentColor,
                    fontSize: 21,
                    fontWeight: FontWeight.bold),
                textAlign: TextAlign.center),
            Text(title,
                style: TextStyle(
                    color: Colors.grey,
                    fontSize: 21,
                    fontWeight: FontWeight.bold),
                textAlign: TextAlign.center),
            Text(room,
                style: TextStyle(
                    color: Colors.grey,
                    fontSize: 21,
                    fontWeight: FontWeight.bold),
                textAlign: TextAlign.center),
          ]);
    }

    return Column(
      children: <Widget>[
        Align(
          alignment: Alignment.topLeft,
          child: Container(
              margin: EdgeInsets.only(left: 15, top: 15),
              child: Text(
                "Spotkania na dziś",
                style: TextStyle(fontSize: 30, color: Colors.grey),
              )),
        ),
        Expanded(
            child: ListView.builder(
              itemCount: 15,
          itemBuilder: (index, item) {
            return Container(
                margin: EdgeInsets.only(left: 20, right: 20, top: 20),
                child: _generateElement(
                    1, "9:50 AM", "Poważne spotkanie", "Mount Everest"));
          },
        ))
      ],
    );
  }
}
