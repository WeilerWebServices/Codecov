FROM prom/prometheus:v2.2.1
COPY ./prometheus/etc/prometheus.yml /etc/prometheus/prometheus.yml
ADD entrypoint.sh /entrypoint.sh
ENTRYPOINT ["/entrypoint.sh"]
