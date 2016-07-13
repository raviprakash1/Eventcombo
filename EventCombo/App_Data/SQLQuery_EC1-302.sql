UPDATE BusinessPage 
SET PageContent='<div class="servererrorcode">500</div>
  <div class="servererrormessage">Oops! Looks like we rocked a too hard and broke somehitng...<br>
    <p>Please try the <a href="/">homepage</a></p>
  </div>
<style>
.servererrorcode {
    text-align: center;
    font-size: 50vw;
    color: #eeeeee;
    line-height: 70vw;
    font-weight: 700;
}
.servererrormessage {
    color: #FF0086;
    font-size: 5vw;
    text-align: center;
    position: absolute;
    margin-top: -50%;
    font-weight: 700;
}
</style>'
WHERE PageNameUrl='server-error'

UPDATE BusinessPage 
SET PageContent='<div class="servererrorcode">404</div>
  <div class="servererrormessage">Hmm, this combo is not on our servers...<br>
    Spring cleaning, amirite?! ;)
    <p>Please try the <a href="/">homepage</a> or <a href="/ec/about-us">let us know</a>.</p>
  </div>
<style>
.servererrorcode {
    text-align: center;
    font-size: 50vw;
    color: #eeeeee;
    line-height: 70vw;
    font-weight: 700;
}
.servererrormessage {
    color: #FF0086;
    font-size: 5vw;
    text-align: center;
    position: absolute;
    margin-top: -50%;
    font-weight: 700;
}
</style>'
WHERE PageNameUrl='page-not-found'