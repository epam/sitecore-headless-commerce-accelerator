# -*- mode: ruby -*-
# vi: set ft=ruby :

=begin
	- place your license.xml file same folder as this one
	- install vagrant hostmanager plugin
	- you might want to install SitecoreRootCert.pfx certificate to your host TrustedRootCertificationAuthorities from "c:\certificates\SitecoreRootCert.pfx" to get rid of SSL warnings in browser (may require reboot)
	- you may also want to navigate to your box file system - you can do that by opening "\\<private_network_ip>\c$" using explorer
=end

sitecore_prefix = 'sc10' # sc10 used by default for boxes with Sitecore 10.x.x
sitecore_postfix = 'dev'
vagrant_box_name = 'w16s-sc10-xc10-jss14-hca' # box name added to vagrant for example 'w16s-sc911'
project_name = 'W16S-SC10-XC10-JSS14-HCA' # your project name
vm_hostname = "#{ENV['COMPUTERNAME']}T#{project_name}"
private_network_ip = '192.168.50.4'
vb_name = "#{project_name}"

vagrant_root = File.dirname(__FILE__) # folder with current Vagrantfile
host_serialization_root = "#{vagrant_root}/src" # path on host opprating system, which is hosting Vagrant and VirtualBox
guest_serialization_root = "/inetpub/wwwroot/#{sitecore_prefix}_sc.#{sitecore_postfix}.local/App_Data/unicorn-hca" # path inside VM that you creating

Vagrant.configure('2') do |config|
  config.vm.box = "#{vagrant_box_name}"
  config.vm.define "#{project_name}"
  config.vm.hostname = "#{vm_hostname}"
  config.vm.synced_folder "#{host_serialization_root}", "#{guest_serialization_root}"
  config.vm.communicator = "winrm"

  # Create a private network, which allows host-only access to the machine using a specific IP.
  config.vm.network 'private_network', ip: "#{private_network_ip}", auto_config: true
  
  # Create a forwarded port mapping which allows access to a specific port
  config.vm.network :forwarded_port, guest: 80,   host: 80,   auto_correct: true # http
  config.vm.network :forwarded_port, guest: 443,  host: 443,  auto_correct: true # https
  config.vm.network :forwarded_port, guest: 1433, host: 1433, auto_correct: true # sql
  config.vm.network :forwarded_port, guest: 8983, host: 8983, auto_correct: true # solr
  config.vm.network :forwarded_port, guest: 8172, host: 8172, auto_correct: true # webdeploy
  config.vm.network :forwarded_port, guest: 4020, host: 4020, auto_correct: true # vs 2015 remote debugger
  config.vm.network :forwarded_port, guest: 4022, host: 4022, auto_correct: true # vs 2017 remote debugger
  config.vm.network :forwarded_port, guest: 4024, host: 4024, auto_correct: true # vs 2019 remote debugger
  
  config.vm.network :forwarded_port, guest: 4200, host: 4200, auto_correct: true # Sitecore Commerce ports
  config.vm.network :forwarded_port, guest: 5000, host: 5000, auto_correct: true # Sitecore Commerce ports
  config.vm.network :forwarded_port, guest: 5005, host: 5005, auto_correct: true # Sitecore Commerce ports
  config.vm.network :forwarded_port, guest: 5010, host: 5010, auto_correct: true # Sitecore Commerce ports
  config.vm.network :forwarded_port, guest: 5015, host: 5015, auto_correct: true # Sitecore Commerce ports
  config.vm.network :forwarded_port, guest: 3389, host: 33389, auto_correct: true # rdp
 
  config.hostmanager.enabled = true
  config.hostmanager.manage_host = true
  config.hostmanager.ignore_private_ip = false
  config.hostmanager.aliases = %W(#{sitecore_prefix}_sc.#{sitecore_postfix}.local #{sitecore_prefix}_identityserver.#{sitecore_postfix}.local #{sitecore_prefix}_xconnect.#{sitecore_postfix}.local #{sitecore_prefix}.commerce bizfx.#{sitecore_prefix}.local commerceauthoring.#{sitecore_prefix}.local)

  config.vm.provider 'virtualbox' do |vb|
    vb.name = "#{vb_name}"
    vb.gui = false
	vb.memory = 8000
    vb.cpus = 4
    vb.customize ['modifyvm', :id, '--natdnshostresolver1', 'on']
	vb.customize ["modifyvm", :id, "--ioapic", "on"]
  end
  
  config.vm.box_check_update = false
end


