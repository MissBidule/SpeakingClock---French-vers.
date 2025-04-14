//
//  main.cpp
//  SpeakingClockProto
//
//  Created by MissBidule on 11/04/2025.
//

#include <ctime>
#include <iostream>

const std::string hourNumbers[13] = {"minuit", "une", "deux", "trois", "quatre", "cinq", "six", "sept", "huit", "neuf", "dix", "onze", "midi"};
const std::string minNumbers[12] = {"", "cinq", "dix", "et quart", "vingt", "vingt-cinq", "et demi", "moins vingt-cinq", "moins vingt", "moins le quart", "moins dix", "moins cinq"};

int main ()
{
    time_t timer;
    time(&timer);
    struct tm datetime = *localtime(&timer);
    
    int hour = datetime.tm_hour > 12 ? datetime.tm_hour%12 : datetime.tm_hour;
    int min = datetime.tm_min - datetime.tm_min%5;

    std::cout << "\e[31mIl est " << hourNumbers[hour] << (hour%12 == 0 ? "" : " heure") << (hour%12 > 1 ? "s " : " ") << minNumbers[min/5];
    
    std::cout << std::endl;

  return 0;
}

/*
 \e[31m
 ILNESTODEUX
 QUATRETROIS
 NEUFUNESEPT
 HUITSIXCINQ
 MIDIXMINUIT
 ONZERHEURES
 MOINSOLEDIX
 ETRQUARTPMD
 VINGT-CINQU
 ETSDEMIEPAM
 */
