import 'package:flutter/material.dart';
import './my_meetings.dart';
import 'package:liquid_pull_to_refresh/liquid_pull_to_refresh.dart';
import './last_reservations.dart';
import './rooms.dart';
import './today_meetings.dart';
import 'reservation_stats.dart';


class Dashboard extends StatefulWidget {
  _DashboardState createState() => _DashboardState();
}

class _DashboardState extends State<Dashboard> {
  Future<void> fakeRequest() async {
    return Future.delayed(Duration(seconds: 1), () {});
  }

  _nawigateRoomReserv(){

  }

  Widget _getChild(index) {
    switch (index) {
      case 0:
        return MyMeetings();
        break;

      case 1:
        return TodayMeetings();
        break;

      case 2:
        return RaisedButton(onPressed: _nawigateRoomReserv,
        color: Theme.of(context).primaryColor,
        child: Text("Zarezerwuj salę", style: TextStyle(fontSize: 40, fontWeight: FontWeight.bold, color: Colors.white),),);
        break;

      case 3:
        return Rooms();
        break;

      case 4:
        return LastReservations();
        break;

        case 5:
        return ReservationStats();
        break;

      default:
        return null;
    }
  }

  @override
  Widget build(BuildContext context) {
    return LiquidPullToRefresh(
      showChildOpacityTransition: false,
      onRefresh: fakeRequest,
      color: Theme.of(context).accentColor,
      child: new ListView.builder(
        itemCount: 6,
        itemBuilder: (context, index) {
          return Container(
              margin: EdgeInsets.only(left: 20, right: 20, top: 10, bottom: 10),
              height: 235,
              decoration: BoxDecoration(
                  color: Colors.white,
                  borderRadius: BorderRadius.all(Radius.circular(5)),
                  boxShadow: <BoxShadow>[
                    BoxShadow(
                      color: Colors.grey,
                      offset: Offset(1, 1),
                      blurRadius: 5.0,
                    ),
                  ]),
              child: _getChild(index));
        },
      ),
    );
  }
}
