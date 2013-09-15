"use strict";
//var IMAGES is defined in another file.
var SHIFT, FADE, STEADY;
SHIFT = 10;
FADE = 1500;
STEADY = 5000;

$(document).ready(function(){
  console.log(IMAGES.length);

  doSlides('#leftImage', 0, IMAGES);
  setTimeout(function(){doSlides('#rightImage', 50, IMAGES);}, STEADY);
});

///Do the slideshow for the img element, perpetually.
/// id - css id to use
/// baseOffset - how far left do we go as a percentage
/// files - array of files
function doSlides(id, baseOffset, files){
  var item, lshift, vshift, p0, p1, p2, p3, fraction;

  item = $(id);
  item[0].src = randomItem(files); //there is probably a better way of doing this
  console.log(item[0].src);
  lshift = randomInt(2*SHIFT) - SHIFT;
  vshift = (randomInt(2*SHIFT) - SHIFT);
  fraction = STEADY / (2*FADE + STEADY);
  p0 = {opacity:0, left:(baseOffset + lshift) + '%', top:vshift + '%'};
  p1 = {opacity:0.9, left:(baseOffset + lshift*fraction) + '%', top:(vshift*fraction) + '%'};
  p2 = {opacity:0.9, left:(baseOffset + lshift*(fraction-1)) + '%', top:(vshift*(fraction-1)) + '%'};
  p3 = {opacity:0, left:(baseOffset - lshift) + '%', top: (-1 * vshift) + '%'};

  item.css(p0)
    .animate(p1, FADE, 'linear')
    .animate(p2, STEADY, 'linear')
    .animate(p3, FADE, 'linear', function() {
      doSlides(id, baseOffset, files); //let's see if this blows the stack.
    });

}

///random number from [0-max)
function randomInt(max){
  return Math.floor(Math.random()*max);
}

///Get a random item from an array. yeah, you can break it with []
function randomItem(arr){
  return arr[randomInt(arr.length)];
}
